$(document).ready(function () {
    $('#bookcover-upload').ssi_uploader({
        inForm: true,
        maxNumberOfFiles: 1,
        maxFileSize: 0.5,
        locale: 'fa',
        allowed: ['png', 'jpg'],
    });
    $('#authorCover-upload').ssi_uploader({
        inForm: true,
        maxNumberOfFiles: 1,
        maxFileSize: 0.5,
        locale: 'fa',
        allowed: ['png', 'jpg'],
    });
    $('#blogCover-upload').ssi_uploader({
        inForm: true,
        maxNumberOfFiles: 1,
        maxFileSize: 0.5,
        locale: 'fa',
        allowed: ['png', 'jpg'],
    });
    $('#index-desktop-upload').ssi_uploader({
        inForm: true,
        maxNumberOfFiles: 1,
        maxFileSize: 0.2,
        locale: 'fa',
        allowed: ['png', 'jpg'],
    });
    $('#index-mobile-upload').ssi_uploader({
        inForm: true,
        maxNumberOfFiles: 1,
        maxFileSize: 0.2,
        locale: 'fa',
        allowed: ['png', 'jpg'],
    });
});




