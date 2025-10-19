const path = require("path");
const webpack = require("webpack");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const CopyWebpackPlugin = require("copy-webpack-plugin");
const CssMinimizerPlugin = require("css-minimizer-webpack-plugin");

module.exports = (env) => {
  const isDevBuild = !(env && env.prod);
  return [
    {
      performance: {
        hints: false,
      },
      stats: {
        modules: false,
        entrypoints: false,
        children: false,
      },
      resolve: {
        extensions: [".js"],
      },
      optimization: {
        minimize: true,
        minimizer: [new CssMinimizerPlugin()],
      },
      module: {
        rules: [
          {
            test: /\.(jpg|jpeg|gif|png)$/,
            use: {
              loader: "file-loader",
              options: {
                name: "images/[name].[ext]",
              },
            },
          },
          {
            test: /@fortawesome([^*]+).(woff|woff2|eot|ttf|svg)$/,
            use: {
              loader: "file-loader",
              options: {
                name: "webfonts/[name].[ext]",
              },
            },
          },
          {
            test: /@bcgov([^*]+).(woff)$/,
            use: {
              loader: "file-loader",
              options: {
                name: "fonts/[name].[ext]",
              },
            },
          },
          {
            test: /\.css(\?|$)/,
            use: [
              {
                loader: MiniCssExtractPlugin.loader,
                options: {},
              },
              "css-loader",
            ],
          },
        ],
      },
      entry: {
        vendor: [
          "bootstrap/dist/css/bootstrap.css",
          "vue",
          "@fortawesome/fontawesome-free/css/fontawesome.css",
          "@fortawesome/fontawesome-free/css/regular.css",
          "@fortawesome/fontawesome-free/css/solid.css",
          "@bcgov/bc-sans/css/BCSans.css",
          "bootstrap-datepicker/dist/css/bootstrap-datepicker3.standalone.css",
        ],
      },
      output: {
        path: path.join(__dirname, "wwwroot", "dist"),
        publicPath: "/dist/",
        filename: "[name].js",
        library: "[name]_[fullhash]",
      },
      plugins: [
        new MiniCssExtractPlugin({
          filename: "css/vendor.min.css",
        }),
        new webpack.DllPlugin({
          path: path.join(__dirname, "wwwroot", "dist", "[name]-manifest.json"),
          name: "[name]_[fullhash]",
        }),
        new webpack.ProvidePlugin({
          $: "jquery",
          "window.$": "jquery",
          jQuery: "jquery",
          "window.jQuery": "jquery",
          Popper: "popper.js",
        }),
        new CopyWebpackPlugin({
          patterns: [
            {
              from: "node_modules/jquery/dist/jquery+(.min|).js",
              to: "lib/[name].js",
            },
            {
              from: "node_modules/bootstrap/dist/js/bootstrap+(.min|).js",
              to: "lib/[name].js",
            },
            {
              //Used for tooltips with bootstrap
              from: "node_modules/popper.js/dist/umd/popper.js",
              to: "lib/popper.js",
            },
            {
              // the min version is packaged above (vendor.min.css)
              // Just copy the regular version for Development
              from: "node_modules/bootstrap/dist/css/bootstrap.css",
              to: "css/bootstrap.css",
            },
            {
              // the min version is packaged above (vendor.min.css)
              // Just copy the regular version for Development
              from: "node_modules/@fortawesome/fontawesome-free/css/all.css",
              to: "css/fontawesome.css",
            },
            {
              // the min version is packaged above (vendor.min.css)
              // Just copy the regular version for Development
              from: "node_modules/@bcgov/bc-sans/css/BCSans.css",
              to: "css/BCSans.css",
            },
            {
              // the min version is packaged above (vendor.min.css)
              // Just copy the regular version for Development
              from: "node_modules/bootstrap-datepicker/dist/css/bootstrap-datepicker3.standalone.css",
              to: "css/bootstrap-datepicker3.standalone.css",
            },
            {
              from: "node_modules/spin/dist/spin.min.js",
              to: "lib/spin.min.js",
            },
            {
              from: "node_modules/bootstrap-datepicker/js/bootstrap-datepicker.js",
              to: "lib/bootstrap-datepicker.js",
            },
            {
              from: "node_modules/jquery-validation/dist/jquery.validate+(.min|).js",
              to: "lib/jquery-validate/[name][ext]",
              noErrorOnMissing: true,
            },
            {
              from: "node_modules/jquery-validation-unobtrusive/dist/jquery.validate.unobtrusive+(.min|).js",
              to: "lib/jquery-validation-unobtrusive",
              noErrorOnMissing: true,
            },
          ],
        }),
        new webpack.DefinePlugin({
          "process.env.NODE_ENV": isDevBuild ? '"development"' : '"production"',
        }),
      ],
    },
  ];
};
