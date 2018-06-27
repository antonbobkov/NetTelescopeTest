/*jshint bitwise: true*/
var fs = require('fs');

/**
 * Returns a padded formatted number
 *
 * @param {Number} number
 * @param {Number} size
 * @return {String}
 */
function pad(num, size) {
  var s = "000000000" + num;
  return s.substr(s.length - size);
}

/**
 * Returns the formatted position coordinates
 *
 * @param {Object} position
 * @return {String}
 */
function printRaDec(position) {
  var h = position.ra_int
    , d = Math.floor(0.5 + position.dec_int * (360 * 3600 * 1000 / 4294967296.0))
    , dec_sign
    , message
    , ra_ms
    , ra_s
    , ra_m
    , dec_ms
    , dec_s
    , dec_m;

  if (d >= 0) {
    if (d > 90 * 3600 * 1000) {
      d =  180 * 3600 * 1000 - d;
      h += 0x80000000;
    }
    dec_sign = '+';
  } else {
    if (d < -90 * 3600 * 1000) {
      d = -180 * 3600 * 1000 - d;
      h += 0x80000000;
    }
    d = -d;
    dec_sign = '-';
  }

  h = Math.floor(0.5 + h * (24 * 3600 * 10000 / 4294967296.0));
  ra_ms = h % 10000; h = Math.floor(h / 10000);
  ra_s = h % 60; h = Math.floor(h / 60);
  ra_m = h % 60; h = Math.floor(h / 60);
  h %= 24;

  dec_ms = d % 1000; d = Math.floor(d / 1000);
  dec_s = d % 60; d = Math.floor (d/ 60);
  dec_m = d % 60; d = Math.floor(d / 60);

  message  = "ra = " + pad(h, 2) + 'h';
  message += pad(ra_m, 2) + 'm';
  message += pad(ra_s, 2) + '.';
  message += pad(ra_ms, 4);
  message += " dec = " + dec_sign + pad(d, 2) + 'h';
  message += pad(dec_m, 2) + 'm';
  message += pad(dec_s, 2) + '.';
  message += pad(dec_ms, 3);

  return message;
}

/**
 * Returns an array of all available telescopes
 *
 * @return {Array<String>}
 */
function getAvailableTelescopes() {
  return fs.readdirSync(__dirname + '/telescopes/').map(function (item) {
    return item.slice(0, -3);
  }).sort();
}

/**
 * Returns an array of all available servers
 *
 * @return {Array<String>}
 */
function getAvailableServers() {
  return fs.readdirSync(__dirname + '/servers/').map(function (item) {
    return item.slice(0, -3);
  }).sort();
}

/**
 * Returns a welcome message
 *
 * @param {params} params server and telescope arguments
 * @return {String}
 */
function welcome(params) {

  return  (params.name ? '\x1b[34m' + params.name + '\x1b[0m: ' : '') +
          'Remote \x1b[36m' + params.type + '\x1b[0m control server ' +
          'running at port \x1b[36m' + params.port + '\x1b[0m ' +
          'to a \x1b[33m' + params.telescopeType + '\x1b[0m telescope';

}

/**
 * Returns a disabled message
 *
 * @param {params} params server and telescope arguments
 * @return {String}
 */
function disabled(params) {

  return  '\x1b[30mDisabled remote ' + params.type + ' control server ' +
          'running at port ' + params.port + ' ' +
          'to a ' + params.telescopeType + ' telescope';

}

module.exports = {
  printRaDec: printRaDec
, getAvailableTelescopes: getAvailableTelescopes
, getAvailableServers: getAvailableServers

, welcome: welcome
, disabled: disabled
};
