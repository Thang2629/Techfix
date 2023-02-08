<template>
  <a-tooltip title="Xóa khách hàng">
    <a-icon
      type="delete"
      class="btn-delete"
      :theme="'twoTone'"
      :twoToneColor="'#FB4E4E'"
      @click="onDelete()"
    />
  </a-tooltip>
</template>

<script>
export default {
  name: "CustomerDelete",
  props: {
    customerId: {
      type: String,
      default: "",
    },
  },
  methods: {
    onDelete() {
      let _this = this;
      this.$confirm({
        title: "Xác nhận xóa",
        content: "Bạn chắc chắn muốn xóa khách hàng này?",
        onOk() {
          _this.$store
            .dispatch("customers/deleteCustomer", { id: _this.customerId })
            .then((res) => {
              _this.$message.success("Xóa khách hàng thành công!");
              _this.$emit("fetchData");
            })
            .catch((err) => {
              _this.$message.error(err);
            });
        },
      });
    },
  },
};
</script>

<style>
</style>