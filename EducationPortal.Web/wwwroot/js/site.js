async function setUserTheme(theme)
{
    document.documentElement.setAttribute("data-theme", theme);

    fetch(`/Account/SetUserTheme?theme=${encodeURIComponent(theme)}`, {
        headers: { "X-Fetch-Request": "true" }
    }).catch(err => console.error(err));
}

async function setUserLanguage(language)
{
    fetch(`/Account/SetUserLanguage?language=${encodeURIComponent(language)}`, {
        headers: { "X-Fetch-Request": "true" }
    }).then(_ => window.location.reload())
        .catch(err => console.error(err));
}