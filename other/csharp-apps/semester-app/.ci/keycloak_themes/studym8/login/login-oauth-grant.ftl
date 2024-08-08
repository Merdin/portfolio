<#import "template.ftl" as layout>
<@layout.registrationLayout bodyClass="oauth"; section>
  <#if section = "header">
    <#if client.name?has_content>
      ${msg("oauthGrantTitle",advancedMsg(client.name))}
    <#else>
      ${msg("oauthGrantTitle",client.clientId)}
    </#if>
  <#elseif section = "form">
    <@layout.card; section>
      <#if section = "content">
        <div class="toegang-geven row">
          <div class="col 10">
            <div id="kc-oauth" class="content-area toegang-tekst">
              <p>${msg("oauthGrantRequest",layout.formatStudyM8((client.name)!client.clientId))?no_esc}</p>
            </div>
          </div>
        </div>
        <div class="row">
          <div class="col 10">
            <ul>
              <#if oauth.clientScopesRequested??>
                <#list oauth.clientScopesRequested as clientScope>
                  <li>
                    <span>${advancedMsg(clientScope.consentScreenText)}</span>
                  </li>
                </#list>
              </#if>
            </ul>
          </div>
        </div>
        <form class="form-actions toegang-buttons" action="${url.oauthAction}" method="POST">
          <input type="hidden" name="code" value="${oauth.code}">
          <div class="${properties.kcFormGroupClass!}">
            <div id="kc-form-options">
              <div class="${properties.kcFormOptionsWrapperClass!}">
              </div>
            </div>
            <div class="row">
              <div class="col 10">
                <div id="kc-form-buttons">
                  <div class="${properties.kcFormButtonsWrapperClass!}">
                    <input class="${properties.kcButtonClass!} ${properties.kcButtonDefaultClass!} ${properties.kcButtonLargeClass!} mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect weigeren-button" name="cancel" id="kc-cancel" type="submit" value="${msg("doDecline")}"/>
                    <input class="${properties.kcButtonClass!} ${properties.kcButtonPrimaryClass!} ${properties.kcButtonLargeClass!} mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect toestaan-button" name="accept" id="kc-login" type="submit" value="${msg("doAccept")}"/>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </form>
        <div class="clearfix"></div>
      </#if>
    </@layout.card>
  </#if>
</@layout.registrationLayout>
