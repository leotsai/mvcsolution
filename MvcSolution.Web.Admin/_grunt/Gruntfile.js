module.exports = function (grunt) {

    var gen = '../js/_gen/';
    var admin = '../js/admin/';
    var js = '../js/';
    var libs = {
        timeframeSelector: admin + "libs/TimeframeSelector.js",
        frequencySelector: admin + "libs/FrequencySelector.js",
        requestTypes: admin + "libs/requestTypes.js",
        upload: admin + "libs/fileupload.js"
    };

    var pages = {
        admin: {
            users: {
                management: []
            },
            analysis: {
                index: [libs.timeframeSelector, libs.requestTypes],
                platforms: [libs.timeframeSelector, libs.requestTypes]
            },
            picture: {
                index: [libs.upload]
            },
            winservice: {
                index: [libs.upload]
            }
        }
    };


    var concat = {};
    for (var areaName in pages) {
        var area = pages[areaName];
        for (var moduleName in area) {
            var module = area[moduleName];
            for (var page in module) {
                var taskName = areaName + '_' + moduleName + '_' + page;
                var output = gen + 'pages/' + areaName + '/' + moduleName + '/' + page + '.js';
                var inputs = module[page];
                inputs.push(admin + 'pages/' + areaName + '/' + moduleName + '/' + page + '/**.js');
                var task = { files: {} };
                task.files[output] = inputs;
                concat[taskName] = task;
            }
        }
    }

    var core = { files: {} };
    core.files[gen + 'mvcsolution.admin.js'] = [admin + 'core/*.js', admin + 'core/*/*.js', admin + 'core/*/*/*.js'];
    concat.core = core;

    var options = {
        concat: concat,
        uglify: concat,
        watch: {
            debug: {
                files: [
                    admin + '*.js',
                    admin + '*/*.js',
                    admin + '*/*/*.js',
                    admin + '*/*/*/*.js',
                    admin + '*/*/*/*/*.js',
                    admin + '*/*/*/*/*/*.js'
                ],
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