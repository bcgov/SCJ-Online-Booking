const path = require("path");
const fs = require("fs");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const CssMinimizerPlugin = require("css-minimizer-webpack-plugin");
const bundleOutputDir = "./wwwroot/dist";
const { VueLoaderPlugin } = require("vue-loader");

var appBasePath = "./ClientSrc/";

var entries = {};
// We search for index.js files inside basePath folder and make those as entries
fs.readdirSync(appBasePath + "vue/").forEach(function (name) {
  var indexFile = appBasePath + "vue/" + name + "/index.js";
  if (fs.existsSync(indexFile)) {
    entries["/vue/" + name + ".js"] = indexFile;
  }
});

// any scss file that doesn't start with an underscore gets built into
// a css file with the same name
fs.readdirSync(appBasePath + "sass/").forEach(function (file) {
  if (!file.startsWith("_")) {
    var cssFile = file.replace(".scss", "");
    entries["/css/" + cssFile] = appBasePath + "sass/" + file;
  }
});

module.exports = (env) => {
  const isDevBuild = !(env && env.prod);
  return [
    {
      target: ["web", "es5"],
      mode: isDevBuild ? "development" : "production",
      performance: {
        hints: false,
      },
      stats: {
        modules: false,
        entrypoints: false,
      },
      optimization: {
        minimize: !isDevBuild,
        minimizer: [new CssMinimizerPlugin()],
      },
      resolve: {
        extensions: [".js", ".vue", ".scss"],
        alias: {
          vue$: "vue/dist/vue.esm-bundler.js",
        },
      },
      entry: entries,
      devtool: "source-map",
      output: {
        path: path.join(__dirname, bundleOutputDir),
        filename: "[name]",
      },
      module: {
        rules: [
          {
            test: /\.vue$/,
            include: /ClientSrc/,
            loader: "vue-loader",
          },
          {
            test: /\.(scss|css)/,
            use: [
              {
                loader: MiniCssExtractPlugin.loader,
              },
              "css-loader",
              "sass-loader",
            ],
          },
        ],
      },
      plugins: [
        new VueLoaderPlugin(),
        new MiniCssExtractPlugin(),
        new (require("webpack").DefinePlugin)({
          __VUE_OPTIONS_API__: true,
          __VUE_PROD_DEVTOOLS__: false,
          __VUE_PROD_HYDRATION_MISMATCH_DETAILS__: false,
        }),
      ],
    },
  ];
};
