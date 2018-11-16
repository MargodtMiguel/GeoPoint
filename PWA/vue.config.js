module.exports = {
    baseUrl: process.env.NODE_ENV === 'production'
      ? '/geopoint2/'
      : '/',
      pwa: {
        name: 'GeoPoint',
        themeColor: '#16be99',
        msTileColor: '#000000',
        appleMobileWebAppCapable: 'yes',
        appleMobileWebAppStatusBarStyle: '#16be99',
      }
  }