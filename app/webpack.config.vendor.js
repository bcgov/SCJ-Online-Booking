const path = require("path");
const webpack = require("webpack");
const ExtractTextPlugin = require("extract-text-webpack-plugin");
const CopyWebpackPlugin = require("copy-webpack-plugin");
const OptimizeCSSPlugin = require("optimize-css-assets-webpack-plugin");

module.exports = (env) => {
    const extractCSS = new ExtractTextPlugin("css/vendor.min.css");
    const isDevBuild = !(env && env.prod);
    return [
        {
            stats: { modules: false },
            resolve: {
                extensions: [".js"]
            },
            module: {
                rules: [
                    {
                        test: /\.(jpg|jpeg|gif|png)$/,
                        loader: "file-loader?name=images/[name].[ext]"
                    },
                    {
                        test: /\.(woff|woff2|eot|ttf|svg)$/,
                        loader: "file-loader?name=fonts/[name].[ext]"
                    },
                    {
                        test: /\.css(\?|$)/,
                        use: extractCSS.extract(["css-loader"])
                    }
                ]
            },
            entry: {
                vendor: [
                    "bootstrap/dist/css/bootstrap.css",
                    "select2/dist/css/select2.css",
                    "select2-bootstrap-theme/dist/select2-bootstrap.css",
                    "vue"
                ]
            },
            output: {
                path: path.join(__dirname, "wwwroot", "dist"),
                publicPath: "/core/dist/",
                filename: "[name].js",
                library: "[name]_[hash]",
            },
            plugins: [
                extractCSS,
                // Compress extracted CSS.
                new OptimizeCSSPlugin({
                    cssProcessorOptions: {
                        safe: true
                    }
                }),
                new webpack.DllPlugin({
                    path: path.join(__dirname, "wwwroot", "dist", "[name]-manifest.json"),
                    name: "[name]_[hash]"
                }),
                new webpack.ProvidePlugin({
                    $: "jquery",
                    "window.$": "jquery",
                    jQuery: "jquery",
                    "window.jQuery": "jquery"
                }),
                new CopyWebpackPlugin([
                    {
                        from: "node_modules/jquery/dist/jquery+(.min|).js",
                        to: "lib/[name].js",
                        toType: "template"
                    },
                    {
                        from: "node_modules/bootstrap/dist/js/bootstrap+(.min|).js",
                        to: "lib/[name].js",
                        toType: "template"
                    },
                    {
                        // the min version is packaged above.  
                        // Just copy the regular version for Development
                        from: "node_modules/bootstrap/dist/css/bootstrap+(.min|).css",
                        to: "css/[name].css",
                        toType: "template"
                    },
                    {
                        from: "node_modules/select2/dist/js/select2+(.full|)+(.min|).js",
                        to: "lib/[name].js",
                        type: "template"
                    },
                    {
                        // the min version is packaged above.  
                        // Just copy the regular version for Development
                        from: "node_modules/select2/dist/css/select2+(.min|).css",
                        to: "css/[name].css",
                        toType: "template"
                    },
                    {
                        // the min version is packaged above.  
                        // Just copy the regular version for Development
                        from:
                            "node_modules/select2-bootstrap-theme/dist/select2-bootstrap+(.min|).css",
                        to: "css/[name].css",
                        toType: "template"
                    },
                ]),
                new webpack.DefinePlugin({
                    'process.env.NODE_ENV': isDevBuild ? '"development"' : '"production"'
                })
            ].concat(isDevBuild
                ? []
                : [
                    new webpack.optimize.UglifyJsPlugin()
                ])
        }
    ];
};
