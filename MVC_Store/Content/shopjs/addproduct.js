$(function () {
    /* Preview selected image */
    function readUrl(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $("img#imgpreview").attr("src", e.target.result).width(170).height(170);
            }

            reader.readAsDataURL(input.files[0]);
        }
    }
    $("#imageUpload").change(function () {
        readUrl(this);
    });

    /////////////////DROPZONE//////////////////
    Dropzone.options.dropzoneForm = {
        acceptedFiles: "images/*",
        init: function () {
            this.on("complete",
                function (file) {
                    if (this.getUploadingFiles().length === 0 && this.getQueuedFiles().length === 0) {
                        location.reload();
                    }
                });
            this.on("sending",
                function (file, xhr, formData) {
                    formData.append("id",  @Model.Id);
        }
    }
});