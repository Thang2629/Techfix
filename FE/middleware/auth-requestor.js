export default async function({ $auth, redirect, store }) {
  let user = $auth.user;
  if (user && user.role === "requestor") {
    // let the user in
  } else {
    redirect(store.state.menu.nav_items[0].path);
  }
}
