
export default async function({$auth, route, redirect, store}) {
  if($auth.loggedIn){
    store.dispatch("menu/findNavItems", $auth.user.role);
    redirect(store.state.menu.nav_items[0].path);
  }
}
