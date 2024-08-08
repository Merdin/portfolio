<#import "template.ftl" as layout>
<@layout.registrationLayout displayMessage=false; section>
  <#if section = "header">
    Welcome terug!
  <#elseif section = "form">

    <@layout.card; section>
      <#if section = "header">
      <#elseif section = "content">
        <form action="${url.loginAction}" method="POST">

          <div class="text-container center-content">
            <p>
              Welkom terug<#if displayName??> ${displayName}</#if>,
            </p>
            <#if email??>
              <p>
                <span class="oranje_tekst">${email}</span>
              </p>
            </#if>
          </div>

          <div class="text-container center-content">
            <p>
              Verder gaan naar de applicatie:
            </p>
          </div>

          <p>
            <div class="buttons-row">
              <div class="buttons">
                <div class="button-container">
                  <button class="mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect loginbutton" name="new-session" type="submit">
                    Met een ander account
                  </button>
                </div>
              </div>
            </div>
          </p>

          <p>
            <div class="buttons-row">
              <div class="buttons">
                <div class="button-container">
                  <button name="next" type="submit" class="mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect mdl-button--accent loginbutton">
                    Als mijzelf
                  </button>
                </div>
              </div>
            </div>
          </p>

        </form>

      </#if>
    </@layout.card>

  </#if>
</@layout.registrationLayout>