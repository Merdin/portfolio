window.addEventListener('load', (event) => {
   let exploringItChoiceValue = null;
   let profileChoiceOneValue = null;
   let profileChoiceTwoValue = null;
   let freeChoiceOneValue = null;
   let freeChoiceTwoValue = null;
   let freeChoiceThreeValue = null;

   const createButton = document.getElementById("createStudyPlan");
   const updateButton = document.getElementById("updateStudyPlan");
   
   const fetchChoices = () => {
      exploringItChoiceValue = document.querySelector("#ExploringItUL > li").dataset.learningUnitId;
      profileChoiceOneValue = document.querySelector("#ProfileChoiceOneUL > li").dataset.learningUnitId;
      profileChoiceTwoValue = document.querySelector("#ProfileChoiceTwoUL > li").dataset.learningUnitId;
      freeChoiceOneValue = document.querySelector("#FreeChoiceOneUL > li").dataset.learningUnitId;
      freeChoiceTwoValue = document.querySelector("#FreeChoiceTwoUL > li").dataset.learningUnitId;
      freeChoiceThreeValue = document.querySelector("#FreeChoiceThreeUL > li").dataset.learningUnitId;
   }
   
   const handleUpdate = (event) => {
      event.preventDefault();
      fetchChoices();
   }
   
   const handleCreate = (event) => {
      event.preventDefault();
      fetchChoices();
      document.body.innerHTML += `<form id="dynForm" action="/StudyPlan/Create" method="post">
                                    <input type="hidden" name="ExploringIt" value="${parseInt(exploringItChoiceValue)}">
                                    <input type="hidden" name="ProfileChoiceOne" value="${profileChoiceOneValue}">
                                    <input type="hidden" name="ProfileChoiceTwo" value="${profileChoiceTwoValue}">
                                    <input type="hidden" name="FreeChoiceOne" value="${freeChoiceOneValue}">
                                    <input type="hidden" name="FreeChoiceTwo" value="${freeChoiceTwoValue}">
                                    <input type="hidden" name="FreeChoiceThree" value="${freeChoiceThreeValue}">
                                  </form>`;
      document.getElementById("dynForm").submit();
   }

   if (createButton) {
      createButton.addEventListener("click", handleCreate);
      createButton.addEventListener("touchstart", handleCreate);
   }
   
   if (updateButton) {
      updateButton.addEventListener("click", handleUpdate);
      updateButton.addEventListener("touchstart", handleUpdate);
   }
   
});