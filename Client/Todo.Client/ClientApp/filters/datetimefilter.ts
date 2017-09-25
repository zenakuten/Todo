import Vue from 'vue'
import moment from 'moment'

Vue.filter('datetime', function(value:string, format:string) {
  if (value) {
    return moment(String(value)).format(format)
  }
});
