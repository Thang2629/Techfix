import RSVP from 'rsvp'

export default async function({store, route}){
  if(!store.state.isLoaded){
    const page = route.query.page || store.state.requests.currentPage
    await RSVP.all([
      store.dispatch('requests/setCurrentPage', page)
    ])
    store.commit('FINISH_LOADING')
  }
}