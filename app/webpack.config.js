const path = require("path");
const webpack = require("webpack");
const fs = require("fs");
const ExtractTextPlugin = require("extract-text-webpack-plugin");
const bundleOutputDir = "./wwwroot/dist";
const CssoWebpackPlugin = require('csso-webpack-plugin').default;

var appBasePath = "./ClientSrc/";

var entries = {};
// We search for index.js files inside basePath folder and make those as entries
fs.readdirSync(appBasePath + "vue/").forEach(function(name) {
    var indexFile = appBasePath + "vue/" + name + "/index.js";
    if (fs.existsSync(indexFile)) {
        entries["/vue/" + name + ".js"] = indexFile;
    }
});

// any scss file that doesn't start with an underscore gets built into 
// a css file with the same name
fs.readdirSync(appBasePath + "sass/").forEach(function (file) {
    if (!file.startsWith("_")) {
        var cssFile = file.replace(".scss", ".css");
        entries["/css/" + cssFile] = appBasePath + "sass/" + file;
    }
});

module.exports = (env) => {
    const isDevBuild = !(env && env.prod);
    return [{
        stats: { modules: false },
        resolve: {
            extensions: ['.js', '.vue', ',scss'],
            alias: {
                'vue$': 'vue/dist/vue'
            }
        },
        entry: entries,
        devtool: "source-map",
        output: {
            path: path.join(__dirname, bundleOutputDir),
            filename: "[name]"
        },
        module: {
            rules: [
                {
                    test: /\.vue$/,
                    include: /ClientSrc/,
                    loader: 'vue-loader',
                    options: { loaders: { js: { loader: 'babel-loader', options: { presets: 'es2015' } } } }
                },
                { test: /\.js$/, include: /ClientSrc/, use: "babel-loader?presets=es2015" },
                { test: /\.(css|scss)/, use: ExtractTextPlugin.extract(['css-loader', 'sass-loader']) }
            ]
        },
        plugins: [
            new webpack.DllReferencePlugin({
                context: __dirname,
                manifest: require("./wwwroot/dist/vendor-manifest.json")
            }),
            new ExtractTextPlugin({
                filename: "[name]"
            }),
            new CssoWebpackPlugin({
                 pluginOutputPostfix: 'min'
            })
        ].concat(isDevBuild ? [
            // Plugins that apply in development builds only
            new webpack.SourceMapDevToolPlugin({
                filename: '[file].map', // Remove this line if you prefer inline source maps
                moduleFilenameTemplate: path.relative(bundleOutputDir, "[resourcePath]") // Point sourcemap entries to the original file locations on disk
            })
        ] : [
                // Plugins that apply in production builds only
                new webpack.optimize.UglifyJsPlugin(),
                new ExtractTextPlugin("site.css")
            ])
    }];
};
