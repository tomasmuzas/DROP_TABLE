const HtmlWebpackPlugin = require('html-webpack-plugin');
const path = require('path');

const appDir = path.resolve(__dirname, 'app/');
const buildDir = path.resolve(__dirname, 'wwwroot/dist/');

const HTMLWebpackPluginConfig = new HtmlWebpackPlugin({
  template: path.resolve(__dirname, 'public/index.html'),
  hash: true,
  filename: 'index.html',
  inject: 'body'
});

module.exports = {
  devtool: 'source-map',
  entry: {
    main: path.resolve(appDir, 'index.js')
  },
  output: {
    path: buildDir,
    filename: 'index_bundle.js',
    publicPath: '/dist/'
  },
  module: {
    loaders: [
      {
        test: /\.js$/,
        include: appDir,
        use: {
          loader: 'babel-loader',
          options: {
            presets: ['env', 'react'],
            plugins: [
              'react-hot-loader/babel',
              'babel-plugin-transform-class-properties',
              'babel-plugin-transform-object-rest-spread'
            ]
          }
        }
      },
      {
        test: /\.css$/,
        loader: 'style-loader!css-loader'
      },
    ]
  },
  plugins: [HTMLWebpackPluginConfig],
  devServer: {
    historyApiFallback: true,
    contentBase: buildDir,
    hot: true,
  }
};
