const path = require("path");
const webpack = require("webpack");
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const CopyWebpackPlugin = require("copy-webpack-plugin");
const CssMinimizerPlugin = require("css-minimizer-webpack-plugin");
const RemovePlugin = require('remove-files-webpack-plugin');

module.exports = (env) => {
    const isDevBuild = !(env && env.prod);
    return [
        {
            performance: {
                hints: false
            },
            stats: {
                modules: false,
                entrypoints: false,
                children: false
            },
            resolve: {
                extensions: [".js"]
            },
            optimization: {
                minimize: true,
                minimizer: [
                    new CssMinimizerPlugin(),
                ],
            },
            module: {
                rules: [
                    {
                        test: /\.(jpg|jpeg|gif|png)$/,
                        use: {
                            loader: "file-loader",
                            options: {
                                name: 'images/[name].[ext]',
                            }
                        }
                    },
                    {
                        test: /@fortawesome([^*]+).(woff|woff2|eot|ttf|svg)$/,
                        use: {
                            loader: "file-loader",
                            options: {
                                name: 'webfonts/[name].[ext]',
                            }
                        }
                    },
                    {
                        test: /@bcgov([^*]+).(woff)$/,
                        use: {
                            loader: "file-loader",
                            options: {
                                name: 'fonts/[name].[ext]',
                            }
                        }
                    },
                    {
                        test: /\.css(\?|$)/,
                        use: [
                            {
                                loader: MiniCssExtractPlugin.loader,
                                options: {}
                            }, 'css-loader'
                        ]
                    }
                ]
            },
            entry: {
                vendor: [
                    "bootstrap/dist/css/bootstrap.css",
                    "vue",
                    "@fortawesome/fontawesome-free/css/fontawesome.css",
                    "@fortawesome/fontawesome-free/css/regular.css",
                    "@fortawesome/fontawesome-free/css/solid.css",
                    "@bcgov/bc-sans/css/BCSans.css",
                    "swiper/css/swiper.css",
                    "bootstrap-datepicker/dist/css/bootstrap-datepicker3.standalone.css"
                ]
            },
            output: {
                path: path.join(__dirname, "wwwroot", "dist"),
                publicPath: "/dist/",
                filename: "[name].js",
                library: "[name]_[fullhash]",
            },
            plugins: [
                new MiniCssExtractPlugin({
                    filename: "css/vendor.min.css"
                }),
                new webpack.DllPlugin({
                    path: path.join(__dirname, "wwwroot", "dist", "[name]-manifest.json"),
                    name: "[name]_[fullhash]"
                }),
                new webpack.ProvidePlugin({
                    $: "jquery",
                    "window.$": "jquery",
                    jQuery: "jquery",
                    "window.jQuery": "jquery",
                    "Popper": "popper.js"
                }),
                new CopyWebpackPlugin({
                    patterns: [
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
                        //Used for tooltips with bootstrap
                        from: "node_modules/popper.js/dist/umd/popper.js",
                        to: "lib/popper.js",
                        toType: "template"
                    },
                    {
                        // the min version is packaged above (vendor.min.css)
                        // Just copy the regular version for Development
                        from: "node_modules/bootstrap/dist/css/bootstrap.css",
                        to: "css/bootstrap.css",
                        toType: "file"
                    },
                    {
                        // the min version is packaged above (vendor.min.css) 
                        // Just copy the regular version for Development
                        from: "node_modules/@fortawesome/fontawesome-free/css/all.css",
                        to: "css/fontawesome.css",
                        toType: "file"
                    },
                    {
                        // the min version is packaged above (vendor.min.css) 
                        // Just copy the regular version for Development
                        from: "node_modules/@bcgov/bc-sans/css/BCSans.css",
                        to: "css/BCSans.css",
                        toType: "file"
                    },
                    {
                        // the min version is packaged above (vendor.min.css) 
                        // Just copy the regular version for Development
                        from:
                            "node_modules/swiper/css/swiper.css",
                        to: "css/swiper.css",
                        toType: "file"
                    },
                    {
                        // the min version is packaged above (vendor.min.css) 
                        // Just copy the regular version for Development
                        from: "node_modules/bootstrap-datepicker/dist/css/bootstrap-datepicker3.standalone.css",
                        to: "css/bootstrap-datepicker3.standalone.css",
                        toType: "file"
                    },
                    {
                        from: "node_modules/spin/dist/spin.min.js",
                        to: "lib/spin.min.js",
                        toType: "file"
                    },
                    {
                        from: "node_modules/bootstrap-datepicker/js/bootstrap-datepicker.js",
                        to: "lib/bootstrap-datepicker.js",
                        toType: "file"
                    },
                ]}),
                new webpack.DefinePlugin({
                    'process.env.NODE_ENV': isDevBuild ? '"development"' : '"production"'
                }),
                new RemovePlugin({
                    after: {
                        root: './wwwroot/dist',
                        include: [],
                        test: [
                            {
                                folder: './',
                                method: (absoluteItemPath) => {
                                    return (new RegExp(/\.(eot|svg|ttf|woff|woff2)$/, 'm').test(absoluteItemPath));
                                }
                            }
                        ],
                    }
                })
            ]
        }
    ];
};
