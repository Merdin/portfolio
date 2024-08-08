// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
  $(".search-multi-select").each(function () {
    $(this).select2();
    $(this).on("select2:opening select2:closing", function (e) {
      var searchField = $(this).parent().find(".select2-search__field");
      // searchField.prop('disabled', true);
    });
  });
});

function searchBar() {
  var input, filter, table, tr, td, i;
  input = document.getElementById("searchInput");
  filter = input.value.toUpperCase();
  table = document.getElementById("searchTable");
  tr = table.getElementsByTagName("tr");
  for (i = 1; i < tr.length; i++) {
    // Hide the row initially.
    tr[i].style.display = "none";

    td = tr[i].getElementsByTagName("td");
    for (let j = 0; j < td.length; j++) {
      var cell = tr[i].getElementsByTagName("td")[j];
      if (cell) {
        if (cell.innerHTML.toUpperCase().indexOf(filter) > -1) {
          tr[i].style.display = "";
          break;
        }
      }
    }
  }
}

function openUrlWithId(url, id) {
  document.location.href = url + id;
}