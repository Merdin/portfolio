<#import "template.ftl" as layout>
<@layout.registrationLayout ; section>
<#if section = "header">
${msg("doLogIn")}
<#elseif section = "form">

<@layout.card; section>
<#if section = "header">
<div class="center-content">
  <a href="#no-account" class="show-dialog no-account">Log in met je StudyM8 account<span class="material-icons">info</span></a>
</div>
<#elseif section = "content">
<#if realm.password>
<form onsubmit="login.disabled = true; return true;" action="" method="post">

  <#if client?? && client.baseUrl?has_content>
    <p><a id="backToApplication" href="${client.baseUrl}">${kcSanitize(msg("backToApplication"))?no_esc}</a></p>
  </#if>

</form>
</#if>

</#if>
</@layout.card>

<dialog class="mdl-dialog" id="no-account">
  <div class="mdl-dialog__actions">
    <button type="button" class="mdl-button close mdl-js-button mdl-button--raised mdl-js-ripple-effect mdl-button--accent">Ok</button>
  </div>
</dialog>


</#if>

</@layout.registrationLayout>
