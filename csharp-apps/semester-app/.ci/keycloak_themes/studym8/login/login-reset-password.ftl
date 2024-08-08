<#import "template.ftl" as layout>
<@layout.registrationLayout displayInfo=true; section>
  <#if section = "header">
    ${msg("emailForgotTitle")}
  <#elseif section = "form">
    <@layout.card; section>
      <#if section = "header">
      <#elseif section = "content">
        <form id="kc-reset-password-form" action="${url.loginAction}" method="post">
          <p class="text-container center-content">Wachtwoord vergeten? <br> Verzoek een reset van je wachtwoord.
          </p>
          <div class="textfield emailfield">
            <div class="material-icons person">
              person
            </div>
            <div class="mdl-textfield mdl-js-textfield">
              <input type="text" id="username" name="username" class="mdl-textfield__input" autofocus/>
              <label class="mdl-textfield__label" for="username">E-mailadres</label>
            </div>
          </div>
          <div class="buttons-row">
            <div class="buttons">
              <div class="button-container max-width">
                <button class="mdl-button ghost" onclick="window.location.href='${url.loginUrl}'" name="back" id="kc-back" type="button">
                  Terug
                </button>
              </div>
              <div class="button-container">
                <button class="mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect mdl-button--accent" name="login" id="kc-login" type="submit">
                  Doorgaan
                </button>
              </div>
            </div>
          </div>
          <div class="center-content my-20">
            <a href="#forgot-username" class="show-dialog forgot-username oranje_tekst md-40">
              Gebruikersnaam vergeten?
            </a>
          </div>
        </form>
      </#if>
    </@layout.card>

<dialog class="mdl-dialog" id="forgot-username">
  <div class="mdl-dialog__content">
    <p>
      Neem contact op met de de administrator binnen je organisatie of anders met het StudyM8 support team voor meer informatie.<br>
      Mail naar<br><a href="mailto:support@studym8.nl?subject=Gebruikersnaam vergeten">
      <span class="oranje_tekst">support@studym8.nl</span><br></a>
      of bel<br>
      <a href="tel:31887732580"><span class="oranje_tekst">088-7732580</span></a>
    </p>
  </div>
  <div class="mdl-dialog__actions">
    <button type="button" class="mdl-button close mdl-js-button mdl-button--raised mdl-js-ripple-effect mdl-button--accent">OK, GA TERUG</button>
  </div>
</dialog>
  </#if>
</@layout.registrationLayout>
