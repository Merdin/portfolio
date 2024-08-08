<#import "template.ftl" as layout>
<@layout.registrationLayout displayInfo=true; section>
  <#if section = "header">
    ${msg("updatePasswordTitle")}
  <#elseif section = "form">
    <@layout.card; section>
      <#if section = "header">
      <#elseif section = "content">
        <form id="kc-passwd-update-form" action="${url.loginAction}" method="post">
          <div class="textfield max-width">
            <span class="nowrap">
              <div class="material-icons">
                https
              </div>
              <div class="mdl-textfield mdl-js-textfield">
                <input class="mdl-textfield__input" type="password" id="password-new" name="password-new" autocomplete="new-password">
                <label class="mdl-textfield__label" for="password-new">
                  ${msg("passwordNew")}
                </label>
              </div>
            </span>
          </div>

          <div class="textfield max-width">
            <span class="nowrap">
              <div class="material-icons">
                https
              </div>
              <div class="mdl-textfield mdl-js-textfield">
                <input class="mdl-textfield__input" type="password" id="password-confirm" name="password-confirm">
                <label class="mdl-textfield__label" for="password-confirm">
                  ${msg("passwordConfirm")}
                </label>
              </div>
            </span>
          </div>
          <div id="kc-form-buttons" class="buttondiv confirm-pw-button max-width mb-20">
            <input class="mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect mdl-button--accent width-100" type="submit" value="${msg("doSubmit")}"/>
          </div>
        </form>
      </#if>
    </@layout.card>
  </#if>
</@layout.registrationLayout>
