/* eslint-disable */
const path = require('path')
const webpack = require('webpack')

function resolve(dir) {
    return path.join(__dirname, dir)
}

// vue.config.js
module.exports = {
    /*
      Vue-cli3:
      Crashed when using Webpack `import()` #2463
      https://github.com/vuejs/vue-cli/issues/2463

     */
    /*
    pages: {
      index: {
        entry: 'src/main.js',
        chunks: ['chunk-vendors', 'chunk-common', 'index']
      }
    },
    */
    configureWebpack: {
        plugins: [
            // Ignore all locale files of moment.js
            new webpack.IgnorePlugin(/^\.\/locale$/, /moment$/)
        ]
    },

    chainWebpack: (config) => {
        config.resolve.alias
            .set('@$', resolve('src'))
            .set('@api', resolve('src/api'))
            .set('@assets', resolve('src/assets'))
            .set('@comp', resolve('src/components'))
            .set('@views', resolve('src/views'))
            .set('@layout', resolve('src/layout'))
            .set('@static', resolve('src/static'))
        const svgRule = config.module.rule('svg')
        svgRule.uses.clear()
        svgRule
            .oneOf('inline')
            .resourceQuery(/inline/)
            .use('vue-svg-icon-loader')
            .loader('vue-svg-icon-loader')
            .end()
            .end()
            .oneOf('external')
            .use('file-loader')
            .loader('file-loader')
            .options({
                name: 'assets/[name].[hash:8].[ext]'
            })
    },

    css: {
        loaderOptions: {
            less: {
                modifyVars: {
                    /* less 变量覆盖，用于自定义 ant design 主题 */

                    /*
                    'primary-color': '#F5222D',
                    'link-color': '#F5222D',
                    'border-radius-base': '4px',
                    */
                },
                javascriptEnabled: true
            }
        }
    },

    devServer: {
        proxy: {
            '/api': {
                //target:'https://www.easy-mock.com/mock/5c3c53b5e477ea245d3601bc/example',
                //target: 'http://localhost:60319',
                target: 'http://192.168.1.117',
                ws: false,
                changeOrigin: true
            },
            '/gateway': {
                target: 'https://www.easy-mock.com/mock/5b7bce071f130e5b7fe8cd7d/antd-pro',
                ws: false,
                changeOrigin: true,
                pathRewrite: {
                    '^/gateway': '/api'
                }
            }
        }
    },

    lintOnSave: undefined,
    // babel-loader no-ignore node_modules/*
    transpileDependencies: []
}