<template>
  <div>
    <a-tooltip title="Nhấn để cập nhật">
      <a @click="handleOpenDrawer" v-if="detail">{{ detail.c_code }} </a>
    </a-tooltip>

    <a-drawer
      title="Cập nhật khách hàng"
      :width="500"
      :visible="isShowDrawer"
      @close="onClose"
      id="fc-drawer"
      :destroyOnClose="true"
      :getContainer="'.fc-main-drawer'"
    >
      <div style="overflow-y: auto; height: 100%; position: relative">
        <a-form :form="form" @submit="handleSubmit">
          <customer-form :detail="newData" :isEditView="true" />
        </a-form>
      </div>

      <div class="fc-global-drawer__footer fc-v2-drawer-footer">
        <div class="fc-global-drawer__footer--group-buttons">
          <a-button type="default" size="large" @click="onClose">
            Hủy
          </a-button>

          <a-button
            key="submit"
            type="primary"
            size="large"
            :loading="isLoading"
            @click="handleSubmit"
          >
            Cập nhật
          </a-button>
        </div>
      </div>
    </a-drawer>
  </div>
</template>

<script>
import moment from "moment";

import CustomerForm from "../Core/CustomerForm.vue";

export default {
  components: {
    CustomerForm,
  },
  props: {
    detail: {
      type: Object,
      default: {},
    },
    actionVisible: Boolean,
  },
  data() {
    return {
      isLoading: false,
      form: this.$form.createForm(this),
      isShowDrawer: false,
    };
  },
  methods: {
    moment,
    handleOpenDrawer() {
      this.isShowDrawer = true;
    },
    onClose() {
      this.isShowDrawer = false;
    },
    handleSubmit() {
      this.form.validateFields((err, values) => {
        if (!err) {
          this.isLoading = true;
          const birthday = this.moment(values.c_birthday).format("DD/MM/YYYY");
          const payload = { ...values, c_birthday: birthday };
          this.$store
            .dispatch("customers/updateCustomer", {
              id: this.detail.id,
              payload,
            })
            .then(() => {
              this.isLoading = false;
              this.$message.success("Cập nhật khách hàng thành công");
              this.$emit("fetchData");
              this.onClose();
            })
            .catch((err) => {
              this.isLoading = false;
              this.$message.error(err);
            });
        }
      });
    },
  },
  computed: {
    newData() {
      const birthday = this.moment(this.detail.c_birthday, "DD/MM/YYYY");
      const newData = {
        ...this.detail,
        c_birthday: birthday,
      };
      return newData;
    },
  },
};
</script>

<style lang="less"></style>
