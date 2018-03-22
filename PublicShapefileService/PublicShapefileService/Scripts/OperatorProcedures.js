


function AcceptRequest(ctl, event, ceva) {
    event.preventDefault();
    swal({
        title: "Doriti sa confirmati cererea?",
        text: "Please check Information before Submiting!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Confirma",
        cancelButtonText: "Cancel",
        closeOnConfirm: false,
        closeOnCancel: false
    },
        function (isConfirm) {
            if (isConfirm) {
                // if (validateData() == true) {
                // $("#CreateForm").submit();

                //  window.location.href = "@Url.Action('AcceptRequest', 'Operator',new {OperatorName=ceva})"; 
                //http://localhost/PublicShapefileService/Operator/AcceptRequest
                $('form').attr('action', "/PublicShapefileService/Operator/AcceptRequest").submit();
                //  $("form").submit();

            } else {
                swal("Cancelled", "You have Cancelled Form Submission!", "error");
            }
        });
}



function RejectRequest(ctl, event) {
    event.preventDefault();
    swal({
        title: "Doriti sa respingeti cererea?",
        text: "Please check Information before Submiting!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Respinge",
        cancelButtonText: "Cancel",
        closeOnConfirm: false,
        closeOnCancel: false
    },
        function (isConfirm) {
            if (isConfirm) {
                // if (validateData() == true) {
                // $("#CreateForm").submit();
                $('form').attr('action', "/PublicShapefileService/Operator/RejectRequest").submit();
                //  $("form").submit();

            } else {
                swal("Cancelled", "You have Cancelled Form Submission!", "error");
            }
        });
}


