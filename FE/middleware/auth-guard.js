export default async function ({ $auth, route, redirect, store }) {
  const condition = localStorage.getItem("fakeLoginTechfix");
  store.dispatch("menu/findNavItems");

  if (!condition) {
    redirect("/sign_in");
  } else {
    store.dispatch("menu/findNavItems");
    // store.dispatch("menu/findNavItems", $auth.user.role);
    // let meow = store.state.menu.nav_items[0].path;
    // console.log(store.state.menu.nav_items[0].path);
    // redirect('/sign_in')
    // redirect(meow);
  }
}
