var gen = '../js/';
var folder = '../_js-source/';
var libs = {
    listPager: folder + "_libs/listpager.js",
    grid: folder + "_libs/Grid.js",
    fileUpload: folder + "_libs/FileUpload.js",
    webImageUploader: folder + "_libs/WebImageUploader.js",
    arrayExtensions: folder + "_libs/ArrayExtensions.js",
    dateExtensions: folder + "_libs/DateExtensions.js",
    app: {
        weixinRecharger: folder + "_libs/app/WeixinRecharger.js",
        weixinJsSdk: folder + "_libs/app/WeixinJsSdk.js",
        weixinImageUploader: folder + "_libs/app/WeixinImageUploader.js",
        likereader: folder + "_libs/app/LikeReader.js"
    },
    admin: {
        weixinMenu: folder + "_libs/admin/WeixinMenuManager.js"
    },
    pc: {
        iphonePreviewer: folder + "_libs/pc/IphonePreviewer.js",
        imageZoomer: folder + "_Libs/pc/ImageZoomer.js",
        imageViewer: folder + "_libs/pc/ImageViewer.js",
        slider: folder + "_libs/pc/Slider.js",
        timeframeSelector: folder + "_libs/pc/TimeframeSelector.js"
    }
};

function getAdminConfigs() {
    return {
        user: {
            index: [libs.grid, libs.pc.imageZoomer, libs.pc.slider, libs.pc.imageViewer]
        },
        role: {
            index: [libs.grid, libs.pc.imageZoomer, libs.pc.slider, libs.pc.imageViewer]
        },
        setting: {
            index: []
        }
    };
}

function getPublicConfigs() {
    return {
        account: {
            "register-step1": [],
            "register-step2": [libs.fileUpload, libs.webImageUploader],
            login: []
        }
    };
};

module.exports = function (grunt) {

    var pages = {
        admin: getAdminConfigs(),
        'public': getPublicConfigs()
    };

    var concat = {};
    for (var areaName in pages) {
        var area = pages[areaName];
        for (var moduleName in area) {
            var module = area[moduleName];
            for (var page in module) {
                var taskName = areaName + '_' + moduleName + '_' + page;
                var output = "../areas/" + areaName + '/js/' + moduleName + '/' + page + '.js';
                var inputs = module[page];
                inputs.push(folder + areaName + '/' + moduleName + '/' + page + '/*.js');
                var task = { files: {} };
                task.files[output] = inputs;
                concat[taskName] = task;
            }
        }
    }

    var core = { files: {} };
    core.files["../areas/public/js/core.js"] = [folder + '_core/*.js', folder + '_core/*/*.js'];
    core.files["../areas/admin/js/core.js"] = [folder + '_core/*.js', folder + '_core/*/*.js'];

    concat.core = core;

    var options = {
        concat: concat,
        uglify: concat,
        watch: {
            debug: {
                files: [
                    folder + '*.js',
                    folder + '*/*.js',
                    folder + '*/*/*.js',
                    folder + '*/*/*/*.js',
                    folder + '*/*/*/*/*.js',
                    folder + '*/*/*/*/*/*.js',
                    folder + '*/*/*/*/*/*/*.js'],
                tasks: ['debug']
            }
        }
    };


    grunt.initConfig(options);

    grunt.loadNpmTasks('grunt-contrib-concat');
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-watch');

    grunt.registerTask('debug', ['concat']);
    grunt.registerTask('release', ['uglify']);
};