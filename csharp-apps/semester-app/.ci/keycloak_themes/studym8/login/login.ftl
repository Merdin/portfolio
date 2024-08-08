<#import "template.ftl" as layout>
<@layout.registrationLayout displayInfo=social.displayInfo; section>
<link href="https://fonts.googleapis.com/css?family=Roboto:400,500" rel="stylesheet">
<#if section = "header">
${msg("doLogIn")}
<#elseif section = "form">

  <@layout.card; section>
    <#if section = "header">
      <div class="center-content">
        <a href="#no-account" class="show-dialog no-account">Log in met je account</a>
      </div>
    <#elseif section = "content">
      <#if realm.password>
      <form onsubmit="login.disabled = true; return true;" action="${url.loginAction}" method="post">
        <div class="textfield">
          <div class="material-icons person">
            person
          </div>
          <div class="mdl-textfield mdl-js-textfield">
            <input class="mdl-textfield__input" type="email" id="username" name="username" value="${(login.username!'')}">
            <label class="mdl-textfield__label" for="username">E-mailadres</label>
          </div>
        </div>
        <div class="textfield">
          <span class="nowrap">
            <div class="material-icons">
              https
            </div>
            <div class="mdl-textfield mdl-js-textfield">
              <input class="mdl-textfield__input" type="password" id="password" name="password">
              <label class="mdl-textfield__label" for="password">
                Wachtwoord
              </label>
            </div>
          </span>
        </div>
        <div class="buttondiv">
          <input tabindex="4" class="mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect mdl-button--accent loginbutton" name="login" type="submit" id="submitlogin" value="${msg("doLogIn")}"/>
        </div>
        <div class="center-content buttondiv zocial keycloak-oidc">
          <#if realm.password && social.providers??>
          <div id="kc-social-providers" class="${properties.kcFormSocialAccountContentClass!} ${properties.kcFormSocialAccountClass!} compass-button">
            <#list social.providers as p>
            <a href="${p.loginUrl}" id="zocial-${p.alias}" class="zocial ${p.providerId}"> <input tabindex="4" class="mdl-button mdl-js-button" name="login" type="button" value="${msg("doLogInCompass")}"/></a>
            </#list>
          </div>
          </#if>
        </div>
      </form>
      </#if>
    </#if>
  </@layout.card>

<dialog class="mdl-dialog" id="password-reset">
  <div class="mdl-dialog__content">
    <p>
      Neem contact op met de servicedesk voor het resetten van je wachtwoord.
    </p>
  </div>
  <div class="mdl-dialog__actions">
    <button type="button" class="mdl-button close mdl-js-button mdl-button--raised mdl-js-ripple-effect mdl-button--accent">Ok</button>
  </div>
</dialog>

</#if>

</@layout.registrationLayout>
