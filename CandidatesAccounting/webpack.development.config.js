const path = require('path');
const webpack = require('webpack');

module.exports = {
  mode: 'development',
  watch: true,
  entry: {
    main: [
      'babel-polyfill',
      'webpack-hot-middleware/client?path=/__webpack_hmr&timeout=20000',
      path.join(__dirname, 'client', 'main.js')
    ],
  },
  output: {
    path: path.join(__dirname, 'public'),
    publicPath: '/',
    filename: path.join('assets', '[name].js'),
  },

  plugins: [
    new webpack.HotModuleReplacementPlugin(),
    new webpack.NoEmitOnErrorsPlugin(),
    new webpack.IgnorePlugin(/^\.\/locale?!\\ru.js$/, /moment$/)
  ],

  optimization: {
    minimize: false,
    splitChunks: {
      cacheGroups: {
        vendors: {
          test: /[\\/]node_modules\\[*lodash*|react\-dom\-factories]/,
          name: "vendors",
          chunks: "initial"
        }
      }
    }
  },

  module: {
    rules: [
      {
        test: /\.js$/,
        include: path.join(__dirname, 'client'),
        loader: ['babel-loader']
      },
      {
        test: /\.css$/,
        loader: ['style-loader', 'css-loader']
      },
      {
        test: /\.(woff|woff2)(\?v=\d+\.\d+\.\d+)?$/,
        loader: 'file-loader',
        options: {
          name: 'fonts/[name].[ext]'
        }
      }
    ]
  },

  devServer: {
    contentBase: path.join(__dirname, '/public'),
    hot: true,
    port: 3000,
  }
};