<template>
  <a-form layout="vertical" :form="form" @submit="handleLoginSubmit">
    <a-form-item label="Email Address">
      <a-input
        v-decorator="[
          'email',
          {
            rules: [{ required: true, message: 'Please input your email!' }],
          },
        ]"
        placeholder="Enter Email Address"
        size="large"
      />
    </a-form-item>
    <a-form-item label="Password">
      <a-input-password
        v-decorator="[
          'password',
          {
            rules: [{ required: true, message: 'Please input your Password!' }],
          },
        ]"
        placeholder="Enter Password"
        size="large"
      />
    </a-form-item>
    <a-form-item :style="{ marginBottom: '10px' }">
      <a-button type="primary" block size="large" html-type="submit">
        Sign In
      </a-button>
    </a-form-item>
  </a-form>
</template>

<script>
import { mapState } from "vuex";
export default {
  props: {
    postLoginAction: {
      type: Function,
      default: () => {},
    },
  },
  data() {
    return {
      form: this.$form.createForm(this, { name: "login" }),
      data: {
        email: "techfix.vn@gmail.com",
        password: "techfix@123",
      },
    };
  },
  computed: {
    ...mapState({
      nav_items: (state) => state.menu.nav_items,
    }),
  },
  methods: {
    handleLoginSubmit(e) {
      e.preventDefault();
      this.form.validateFields((err, values) => {
        if (!err) {
          (async () => {
            try {
              if (
                Object.entries(this.data).toString() ===
                Object.entries(values).toString()
              ) {
                localStorage.setItem("fakeLoginTechfix", true);
                this.$message.success("Đăng nhập thành công!");
                this.$store.dispatch("menu/findNavItems");
                this.$router.push("/");
              } else {
                this.$message.error("Kiểm tra lại thông tin đăng nhập!");
              }
              // await this.$auth.login();
              console.log(values);
              // this.$message.success("Logged In!");
              // this.$store.dispatch("menu/findNavItems", this.$auth.user.role);
              // this.$router.push(this.nav_items[0].path);
            } catch (error) {
              this.$message.error("Kiểm tra lại thông tin đăng nhập!");
            }
          })();
        }
      });
    },
  },
  validateInfo(values) {
    const data = {
      email: "techfix.vn@gmail.com",
      password: "techfix@123",
    };
  },
};
</script>

<style lang="less" scoped></style>
