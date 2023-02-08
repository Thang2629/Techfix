<template>
  <div>
    <a-button type="primary" class="fc-group-button blue" @click="showModal">
      + Tạo khách hàng
    </a-button>
    <a-modal
      title="Tạo mới khách hàng"
      :visible="visible"
      :confirm-loading="isLoading"
      @ok="handleSubmit"
      @cancel="handleCancel"
      class="fc-modal"
      :destroyOnClose="true"
    >
      <template slot="footer">
        <a-button key="back" @click="handleCancel" size="large"> Hủy </a-button>
        <a-button
          key="submit"
          type="primary"
          :loading="isLoading"
          @click="handleSubmit"
          size="large"
        >
          Lưu
        </a-button>
      </template>

      <div style="overflow-y: auto; height: 100%; position: relative">
        <a-form :form="form" @submit="handleSubmit">
          <customer-form />
        </a-form>
      </div>
    </a-modal>
  </div>
</template>

<script>
import CustomerForm from "../Core/CustomerForm.vue";
import moment from "moment";

export default {
  name: "CustomerCreate",
  components: {
    CustomerForm,
  },
  data() {
    return {
      visible: false,
      isLoading: false,
      form: this.$form.createForm(this),
    };
  },
  methods: {
    moment,
    showModal() {
      this.visible = true;
    },
    handleCancel(e) {
      this.visible = false;
    },
    handleSubmit() {
      this.form.validateFields((err, values) => {
        if (!err) {
          this.isLoading = true;
          const bd = this.moment(values.c_birthday).format("DD/MM/YYYY");
          this.$store
            .dispatch("customers/createCustomer", {
              ...values,
              c_birthday: bd,
            })
            .then(() => {
              this.isLoading = false;
              this.$message.success("Tạo mới khách hàng thành công");
              this.$emit("fetchData");
              this.handleCancel();
            })
            .catch((err) => {
              this.isLoading = false;
              this.$message.error(err);
            });
        }
      });
    },
  },
};
</script>

<style></style>
