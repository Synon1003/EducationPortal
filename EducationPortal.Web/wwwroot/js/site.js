async function setUserTheme(theme)
{
    document.documentElement.setAttribute("data-theme", theme);

    fetch(`/Account/SetUserTheme?theme=${encodeURIComponent(theme)}`).catch(err => console.error(err));
}