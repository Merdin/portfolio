<#macro registrationLayout bodyClass="" displayInfo=false displayMessage=true>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">

  <head>
    <meta charset="utf-8">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <meta name="robots" content="noindex, nofollow">
    <meta name="viewport" content="width=device-width"> <!-- This is for testing in chrome device emulator -->
    <#if properties.meta?has_content>
        <#list properties.meta?split(' ') as meta>
            <meta name="${meta?split('==')[0]}" content="${meta?split('==')[1]}"/>
        </#list>
    </#if>
    <title>${msg("loginTitle",(realm.displayName!''))}</title>
    <link rel="icon" href="${url.resourcesPath}/img/favicon.ico" />
    <link rel="stylesheet" href="https://fonts.googleapis.com/icon?family=Material+Icons">
    <link rel="stylesheet" href="https://code.getmdl.io/1.3.0/material.indigo-pink.min.css">
    <script defer src="https://code.getmdl.io/1.3.0/material.min.js"></script>
    <#if properties.styles?has_content>
        <#list properties.styles?split(' ') as style>
            <link href="${url.resourcesPath}/${style}?a=${.now?long}" rel="stylesheet" />
        </#list>
    </#if>
    <#if properties.scripts?has_content>
        <#list properties.scripts?split(' ') as script>
            <script src="${url.resourcesPath}/${script}?a=${.now?long}" type="text/javascript"></script>
        </#list>
    </#if>
    <#if scripts??>
        <#list scripts as script>
            <script src="${script}" type="text/javascript"></script>
        </#list>
    </#if>
  </head>

  <body>
    <#nested "form">
    <#if displayInfo>
        <div id="kc-info" class="${properties.kcSignUpClass!}">
            <div id="kc-info-wrapper" class="${properties.kcInfoAreaWrapperClass!}">
                <#nested "info">
            </div>
        </div>
    </#if>

    <script>
      document.querySelectorAll('.show-dialog').forEach(function(showDialogButton) {
        var dialog = document.querySelector(showDialogButton.getAttribute('href'));
        console.log(showDialogButton, dialog);
        if (! dialog.showModal) {
          dialogPolyfill.registerDialog(dialog);
        }
        showDialogButton.addEventListener('click', function(event) {
          console.log('click', dialog);
          event.preventDefault();
          dialog.showModal();
        });
        dialog.querySelectorAll('.close, .backdrop').forEach(function(elem) {
          elem.addEventListener('click', function(event) {
          event.preventDefault();
            dialog.close();
          });
        });
      });
    </script>
  </body>
</html>
</#macro>
<#macro card displayMessage=true>
<div class="card login-card">
  <header>
    <div class="center-content" style="margin-bottom: 20px">
      <img class="logo" src="${url.resourcesPath}/img/logo.png">
    </div>
    <#nested "header">
  </header>

  <main>

    <#if displayMessage>
        <#if message?has_content>
          <div class="alert alert-${message.type}">
            <span class="material-icons error_outline">error_outline</span> ${message.summary?no_esc}
          </div>
        </#if>
    </#if>

    <#nested "content">
  </main>

  <footer>
    <div>
      <div class="copyright center-content" style="margin-bottom: 20px">
        <!--<img src="${url.resourcesPath}/img/logo.png">-->
      </div>
    </div>
  </footer>
</div>

</#macro>