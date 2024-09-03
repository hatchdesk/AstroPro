// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Initialization for ES Users
import { Datatable, initMDB } from "mdb-ui-kit";

initMDB({ Datatable });

    document.getElementById('DropDownCategory').addEventListener('change', function () {
        var selectedCategory = this.value;
        alert("Hello , i am changing")

    document.getElementById('feesBox').style.display = 'block';

        fetch(`https://localhost:7173/Admin/Fee/Detail/2=${encodeURIComponent(selectedCategory)}`)
            .then(response => response.json())
        .then(data => {

        document.getElementById('feesAmount').innerText = `₹${data.fees}`;
            })
            .catch(error => console.error('Error fetching fees:', error));
    });




