<#import "template.ftl" as layout>
<@layout.registrationLayout; section>

  <#if section = "form">
    <@layout.card; section>
      <#if section = "header">
      <#elseif section = "content">
        <form id="terms" action="${url.loginAction}" method="POST">
        	<div>
              <div id="p1" class="mdl-progress mdl-js-progress mdl-progress-orange"></div>
              <script>
                document.querySelector('#p1').addEventListener('mdl-componentupgraded', function() {
                  this.MaterialProgress.setProgress(66);
                });
              </script>
            </div>

          <div class="buttondiv">
            <div class="text-container center-content max-width">
              <p>
                Als laatste stap vragen we je om de
                <a href="#terms-dialog" class="show-dialog">Algemene Voorwaarden</a> te lezen en
                accepteren.
              </p>
              <p>
              </p>
            </div>
          </div>

          <div class="big-button">
            <a href="#terms-dialog" class="mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect mdl-button--fab mdl-button--colored mdl-button--accent show-dialog">
            	<img src="${url.resourcesPath}/img/Gebruiksvoorwaarden_knop_met_cirkel.svg" alt="Klik hier voor de algemene voorwaarden">
            </a>
          </div>

          <div id="term-and-condition-buttons">
              <div class="button-container">
                <button class="mdl-button ghost" name="cancel" type="submit">
                  Weigeren
                </button>
              </div>
              <div class="button-container mb-20">
                <button class="mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect mdl-button--accent" name="accept" type="submit">
                  Accepteren
                </button>
              </div>

          </div>

        </form>
      </#if>
    </@layout.card>
  </#if>

</@layout.registrationLayout>
