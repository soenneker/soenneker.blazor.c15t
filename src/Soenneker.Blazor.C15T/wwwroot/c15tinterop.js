let c15tModulePromise;
let runtime;
let consentStore;

function getModuleUrl(options) {
    return options?.moduleUrl || "https://cdn.jsdelivr.net/npm/c15t@1.8.5/+esm";
}

function normalizeOptions(options) {
    const normalized = { ...(options || {}) };
    delete normalized.moduleUrl;

    if (Array.isArray(normalized.consentCategories) && !normalized.initialGdprTypes)
        normalized.initialGdprTypes = normalized.consentCategories;

    return normalized;
}

async function getModule(moduleUrl) {
    c15tModulePromise ??= import(moduleUrl);
    return await c15tModulePromise;
}

function requireStore() {
    if (!consentStore)
        throw new Error("c15t has not been initialized.");

    return consentStore;
}

function getRawState() {
    return requireStore().getState();
}

function mapState(state) {
    if (!state)
        return null;

    return {
        consents: state.consents ?? null,
        selectedConsents: state.selectedConsents ?? null,
        displayedConsents: typeof state.getDisplayedConsents === "function" ? state.getDisplayedConsents() : null,
        showPopup: state.showPopup ?? null,
        isPrivacyDialogOpen: state.isPrivacyDialogOpen ?? null,
        isLoadingConsentInfo: state.isLoadingConsentInfo ?? null,
        consentInfo: state.consentInfo ?? null,
        locationInfo: state.locationInfo ?? null,
        jurisdictionInfo: state.jurisdictionInfo ?? null
    };
}

async function afterAction(result) {
    if (result && typeof result.then === "function")
        await result;

    return getState();
}

export async function initialize(options) {
    const moduleUrl = getModuleUrl(options);
    const module = await getModule(moduleUrl);
    const runtimeOptions = normalizeOptions(options);

    if (typeof module.getOrCreateConsentRuntime === "function") {
        runtime = module.getOrCreateConsentRuntime(runtimeOptions);
        consentStore = runtime?.consentStore ?? runtime?.store ?? runtime;
    } else {
        const managerMode = runtimeOptions.backendURL
            ? (runtimeOptions.mode === "hosted" ? "c15t" : runtimeOptions.mode)
            : "offline";

        const managerOptions = {
            mode: managerMode,
            backendURL: runtimeOptions.backendURL,
            storageConfig: runtimeOptions.storageConfig,
            headers: runtimeOptions.headers,
            customFetch: runtimeOptions.customFetch,
            corsMode: runtimeOptions.corsMode,
            retryConfig: runtimeOptions.retryConfig
        };

        const manager = module.configureConsentManager
            ? module.configureConsentManager(managerOptions)
            : undefined;

        runtime = { manager };
        consentStore = module.createConsentManagerStore(manager, runtimeOptions);
    }

    if (!consentStore || typeof consentStore.getState !== "function")
        throw new Error("c15t did not return a consent store.");

    return getState();
}

export function getState() {
    return mapState(getRawState());
}

export async function acceptAll() {
    return await afterAction(getRawState().saveConsents?.("all"));
}

export async function rejectNonNecessary() {
    return await afterAction(getRawState().saveConsents?.("necessary"));
}

export async function saveCustom() {
    return await afterAction(getRawState().saveConsents?.("custom"));
}

export async function setConsent(category, value) {
    if (!category)
        throw new Error("category is required.");

    return await afterAction(getRawState().setConsent?.(category, value));
}

export function setSelectedConsent(category, value) {
    if (!category)
        throw new Error("category is required.");

    getRawState().setSelectedConsent?.(category, value);
    return getState();
}

export function openDialog() {
    getRawState().setIsPrivacyDialogOpen?.(true);
    return getState();
}

export function showBanner() {
    getRawState().setShowPopup?.(true, true);
    return getState();
}

export function closeUi() {
    const state = getRawState();
    state.setIsPrivacyDialogOpen?.(false);
    state.setShowPopup?.(false);
    return getState();
}

export function resetConsents() {
    getRawState().resetConsents?.();
    return getState();
}

export function dispose() {
    consentStore = undefined;
    runtime = undefined;
}
