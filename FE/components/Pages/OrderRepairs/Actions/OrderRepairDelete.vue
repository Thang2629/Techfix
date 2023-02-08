<template>
  <a-tooltip title="Xóa đơn">
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
  name: "OrderRepairDelete",
  props: {
    orderRepairId: {
      type: String,
      default: "",
    },
  },
  methods: {
    onDelete() {
      let _this = this;
      this.$confirm({
        title: "Xác nhận xóa",
        content: "Bạn chắc chắn muốn xóa đơn này?",
        onOk() {
          _this.$store
            .dispatch("order_repairs/deleteOrderRepair", {
              id: _this.orderRepairId,
            })
            .then((res) => {
              _this.$message.success("Xóa đơn thành công!");
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

<style></style>
