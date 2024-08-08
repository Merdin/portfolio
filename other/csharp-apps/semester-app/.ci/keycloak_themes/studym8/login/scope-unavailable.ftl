<#import "template.ftl" as layout>
<@layout.registrationLayout displayInfo=social.displayInfo; section>
  <#if section = "header">
    Geen toegang
  <#elseif section = "form">

    <@layout.card; section>
      <#if section = "header">
      <#elseif section = "content">

        <div class="buttondiv max-width">
          <div class="text-container center-content">
            <p>
              Helaas, je kunt met dit account (nog) geen gebruik maken van de applicatie
            </p>
            <p>
              Heb je vragen over het gebruik van StudyM8? Neem dan contact met ons op!
            </p>
          </div>
        </div>

        <div class="button-container max-width center-content buttondiv mt-20 mb-40">
          <a href="${restartAuthenticationUrl}" class="mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect mdl-button--accent">
            Opnieuw inloggen
          </a>
        </div>

      </#if>
    </@layout.card>

  </#if>
</@layout.registrationLayout>