module.exports = function (grunt) {

    var gen = '../MvcSolution.Web.Main/js/_gen/';
    var mvcsolution = '../MvcSolution.Web.Main/js/mvcsolution/';
    var libs = {
        
    };

    var pages = {
        loggedin: {
            user: {
                index: [],
                edit: []
            }
        },
        "public": {
            user: {
                register: []
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
                inputs.push(mvcsolution + 'pages/' + areaName + '/' + moduleName + '/' + page + '/*.js');
                var task = { files: {} };
                task.files[output] = inputs;
                concat[taskName] = task;
            }
        }
    }

    var core = { files: {} };
    core.files[gen + 'mvcsolution.core.js'] = [mvcsolution + 'core/*.js', mvcsolution + 'core/*/*.js'];
    concat.core = core;

    var options = {
        concat: concat,
        uglify: concat,
        watch: {
            debug: {
                files: [
                    mvcsolution + '*.js',
                    mvcsolution + '*/*.js',
                    mvcsolution + '*/*/*.js',
                    mvcsolution + '*/*/*/*.js',
                    mvcsolution + '*/*/*/*/*.js',
                    mvcsolution + '*/*/*/*/*/*.js',
                    mvcsolution + '*/*/*/*/*/*/*.js'],
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