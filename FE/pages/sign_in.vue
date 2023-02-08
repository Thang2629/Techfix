<template>
  <div class="login-form">
    <a-card title="Đăng nhập" :bordered="false" style="width: 400px">
      <user-login-form />
    </a-card>
  </div>
</template>

<script>
import UserLoginForm from "~/components/UserLoginForm.vue";
export default {
  layout: "auth",
  components: { UserLoginForm },
};
</script>
<style lang="less">
.login-form {
  display: flex;
  justify-content: center;
  padding: 5rem 0 10rem 0;
  background: #ececec;
  height: 100vh;
}
#auth_page {
  width: 100vw;
  height: 100vh;
}

#auth_page .auth-left-side {
  display: flex;
  align-items: center;
  justify-content: center;
  height: 100%;
  // background: #f9f9f9;
}

#auth_page .auth-right-side {
  display: flex;
  align-items: center;
  justify-content: center;
  height: 100%;
}

#auth_page .auth-body {
  width: 770px;
  height: 600px;
  display: flex;
  justify-content: space-between;
  flex-direction: column;
}
</style>
