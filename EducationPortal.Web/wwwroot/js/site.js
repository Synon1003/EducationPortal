async function setUserTheme(theme)
{
    document.documentElement.setAttribute("data-theme", theme);

    fetch(`/Account/SetUserTheme?theme=${encodeURIComponent(theme)}`, {
        headers: { "X-Fetch-Request": "true" }
    }).catch(err => console.error(err));
}