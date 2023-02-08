import Vue from 'vue';
import moment from 'moment'
import { library } from '@fortawesome/fontawesome-svg-core'

/* import font awesome icon component */
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'

/* import specific icons */
import { faUserSecret, faBoxesStacked, faUsers, faUser } from '@fortawesome/free-solid-svg-icons'

/* add icons to the library */
library.add(faUserSecret, faBoxesStacked, faUsers, faUser)

/* add font awesome icon component */
Vue.component('font-awesome-icon', FontAwesomeIcon)

Vue.config.productionTip = false

Vue.mixin({
  methods: {
    notEmptyObject(someObject) {
      return Object.keys(someObject).length
    }
  }
})

Vue.filter('formatTimeStamp', function (value) {
  if (value) {
    return moment.unix(value).format('DD/MM/YYYY hh:mm A')
  }
})