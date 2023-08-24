var dataTable;

$(document).ready(function () {
    let searchParams = new URLSearchParams(window.location.search)

    let prefix = "";
    let number = "";
    let name = "";

    let searchQueryString = "";
    let searchQueryArray = new Array();

    // See if the url contains the prefix, number, and/or name
    if (searchParams.has('CourseSearchVM.Course.ProgramPrefix')) {
        if (searchParams.get('CourseSearchVM.Course.ProgramPrefix') != "") {
            prefix = "CoursePrefix=" + searchParams.get('CourseSearchVM.Course.ProgramPrefix');
            searchQueryArray.push(prefix);
        }
    }

    if (searchParams.has('CourseSearchVM.Course.CourseNumber')) {
        if (searchParams.get('CourseSearchVM.Course.CourseNumber') != "") {
            number = "CourseNumber=" + searchParams.get('CourseSearchVM.Course.CourseNumber');
            searchQueryArray.push(number);
        }
    }

    if (searchParams.has('CourseSearchVM.Course.CourseName')) {
        if (searchParams.get('CourseSearchVM.Course.CourseName') != "") {
            name = "CourseName=" + searchParams.get('CourseSearchVM.Course.CourseName')
            searchQueryArray.push(name);
        }
    }

    // If we have atleast on of of the values combine them all
    if (searchQueryArray.length > 0) {
        searchQueryString += "?";

        searchQueryArray.forEach(element => searchQueryString += element + "&");
        // Remove last character

        searchQueryString = searchQueryString.slice(0, -1);
    }

    loadDataTable(searchQueryString);
});

function loadDataTable(status) {
    // Get the semster ID
    let searchParams = new URLSearchParams(window.location.search);
    let SemsterId = "";

    // Because we have to GETS one from Index, and one from itself we have to see which value is passed
    if (searchParams.has('CourseSearchVM.Semester.Id')) {
        if (searchParams.get('CourseSearchVM.Semester.Id') != "") {
            SemsterId = searchParams.get('CourseSearchVM.Semester.Id');
        }
    }
    if (searchParams.has('Semester')) {
        if (searchParams.get('Semester') != "") {
            SemsterId = searchParams.get('Semester');
        }
    }

    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Preferences/GetCourseList" + status
        },
        "columns": [
            { "data": "courseID", defaultContent: "N/A" },
            { "data": "programPrefix", defaultContent: "N/A" },
            { "data": "courseNumber", defaultContent: "N/A" },
            { "data": "courseName", defaultContent: "N/A" },
            { "data": "creditHours", defaultContent: "N/A" },
            { "data": null, "defaultContent": "" },
        ],
        columnDefs: [
            // Add a button to add a course to your wishlist. https://datatables.net/examples/ajax/null_data_source.html
            {
                render: (data, type, row) => `<a class="btn btn-outline-primary" href="/Student/Preferences/Upsert?CourseID=${row['courseID']}&SemesterID=${SemsterId}">Add</a>`,
                targets: -1
            },

            // Hide the ID row
            { visible: false, targets: [0] },
        ]
    });
}
