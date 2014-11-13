module.exports = function (grunt) {
    var libs = {

    };

    var areas = {
        admin: {
            setting: {
                index: []
            }
        },
        "public": {
            account: {
                register: []
            }
        }
    };

    var singles = {
        "../js/mvcsolution.core.js": "../js-source/_core/*.js"
    };

    var concat = {};
    for (var areaName in areas) {
        var area = areas[areaName];
        for (var moduleName in area) {
            var module = area[moduleName];
            for (var page in module) {
                var taskName = areaName + '_' + moduleName + '_' + page;
                var output = '../js/mvcsolution/' + areaName + '/' + moduleName + '/' + page + '.js';
                var inputs = module[page];
                inputs.push('../js-source/' + areaName + '/' + moduleName + '/' + page + '/*.js');
                var task = { files: {} };
                task.files[output] = inputs;
                concat[taskName] = task;
            }
        }
    }
    concat["singles"] = { files: singles };

    var options = {
        concat: concat,
        uglify: concat,
        watch: {
            debug: {
                files: [
                    '../source-js/*.js',
                    '../source-js/*/*.js',
                    '../source-js/*/*/*.js',
                    '../source-js/*/*/*/*.js',
                    '../source-js/*/*/*/*/*.js',
                    '../source-js/*/*/*/*/*/*.js',
                    '../source-js/*/*/*/*/*/*/*.js'
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