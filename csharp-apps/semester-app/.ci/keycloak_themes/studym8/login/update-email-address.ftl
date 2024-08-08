<#import "template.ftl" as layout>
<@layout.registrationLayout; section>

  <#if section = "form">
    <@layout.card; section>
      <#if section = "header">
      <#elseif section = "content">
        <form id="update-email-address" class="login-form" action="${url.loginAction}" method="post">
          <div id="p1" class="mdl-progress mdl-js-progress mdl-progress-orange"></div>
          <script>
            document.querySelector('#p1').addEventListener('mdl-componentupgraded', function() {
              this.MaterialProgress.setProgress(0);
            });
          </script>
          <div class="buttondiv max-width">
            <div class="text-container center-content">
              <p>
                Een StudyM8 account is gekoppeld aan een e-mailadres.
              </p>
              <p>
                Vul je e-mailadres in:
              </p>
            </div>
          </div>

          <div class="textfield max-width">
            <div class="material-icons email">
              email
            </div>
            <div class="mdl-textfield mdl-js-textfield">
              <input class="mdl-textfield__input" type="text" id="email" name="email" value="${(updateEmail.email!'')}">
              <label class="mdl-textfield__label" for="email">E-mailadres</label>
            </div>
          </div>

          <div class="buttonrow max-width">
            <div class="button-container mb-40">
              <button class="mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect mdl-button--accent widebutton" name="login" type="submit">
                Doorgaan
              </button>
            </div>
          </div>
        </form>
      </#if>
    </@layout.card>
  </#if>
</@layout.registrationLayout>
