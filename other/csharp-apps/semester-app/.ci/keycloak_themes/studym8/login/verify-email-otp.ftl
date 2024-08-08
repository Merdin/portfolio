<#import "template.ftl" as layout>
<@layout.registrationLayout; section>

  <#if section = "form">
    <@layout.card; section>
      <#if section = "header">
      <#elseif section = "content">
          <form id="verify-email-otp" class="login-form" action="${url.loginAction}" method="post">
            <div>
              <div id="p1" class="mdl-progress mdl-js-progress mdl-progress-orange"></div>
              <script>
                document.querySelector('#p1').addEventListener('mdl-componentupgraded', function() {
                  this.MaterialProgress.setProgress(33);
                });
              </script>
            </div>
            <div class="buttondiv">
              <div class="text-container center-content">
                <p>
                  Er is een bevestigingscode gestuurd naar <strong>${(email!'')}</strong>.
                </p>
                <p>
                  Vul hier de code in die je in de e-mail aantreft. Zo weten we zeker dat het jouw
                  e-mail adres is.
                </p>
              </div>
            </div>

            <div class="textfield">
              <div class="material-icons lock">
                lock
              </div>
              <div class="mdl-textfield mdl-js-textfield">
                <input class="mdl-textfield__input" type="text" id="token" name="token" value="">
                <label class="mdl-textfield__label" for="token">Code</label>
              </div>

            </div>

            <div class="buttons-row">
              <button class="hidden" name="submitAction" type="submit" value="next"></button>

              <div class="buttons">
                <div class="button-container">
                  <button class="mdl-button mdl-js-button mdl-js-ripple-effect ghost" name="submitAction" id="kc-back" type="submit" value="back">
                    Terug
                  </button>
                </div>
                <div class="button-container">
                  <button class="mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect mdl-button--accent" name="submitAction" id="kc-login" type="submit" value="next">
                    Doorgaan
                  </button>
                </div>
              </div>
            </div>

            <div class="buttondiv">
              <div class="text-container center-content">
                <button class="link-button" name="submitAction" type="submit" value="resend">
                  Code niet ontvangen of werkt niet? Stuur opnieuw.
                </button>
              </div>
            </div>

          </form>
      </#if>
    </@layout.card>
  </#if>

</@layout.registrationLayout>
