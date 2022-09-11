$(document).ready(() => {

    $("#Role").hide();
    $("#Role").val($("#RoleDepartment").val());

    $('#TypeId').change((t) => {
        let selected = t.target.value;

        if (selected === "1") {
            $("#RoleDepartment").show();
            $("#Role").hide();
        } else {
            $("#RoleDepartment").hide();
            $("#Role").show();
        }
    });

    $("#RoleDepartment").change((d) => {
        let department = d.target.value;

        alert(department);

        $("#Role").val(department);
    });
});