const path = require('path');
const webpack = require('webpack');
const CopyWebpackPlugin = require('copy-webpack-plugin');

const __root = path.join(__dirname, '..');

module.exports = {
  mode: 'development',
  watch: true,

  entry: {
    main: [
      'babel-polyfill',
      'webpack-hot-middleware/client?path=/__webpack_hmr&timeout=20000',
      path.join(__root, 'client', 'main.js')
    ],
  },

  output: {
    path: path.join(__root, 'dist', 'public'),
    publicPath: '/',
    filename: path.join('assets', '[name].js')
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
          name: 'vendors',
          chunks: 'initial'
        }
      }
    }
  },

  module: {
    rules: [
      {
        test: /\.js$/,
        include: path.join(__root, 'client'),
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
    contentBase: path.join(__root, 'dist', 'public'),
    hot: true,
    port: 3000,
  }
};