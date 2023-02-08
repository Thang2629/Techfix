<template>
  <div class="repair-customer-select">
    <div class="repair-customer-select--alert">
      <a-alert message="Thông tin khách hàng" type="info" show-icon />
    </div>

    <div class="repair-customer-select--select">
      <a-row>
        <a-col :span="3">
          <div class="label">Khách Hàng:</div>
        </a-col>
        <a-col :span="21">
          <div class="select-button">
            <a-select
              show-search
              :allowClear="true"
              size="large"
              @change="onSelectCustomer"
              @search="handleSearch"
              :loading="isLoading"
              class="fc-select-normal"
              :filter-option="false"
              placeholder="Tìm khách hàng"
            >
              <a-select-option
                v-for="customer in dataCustomersSelect"
                :key="customer.id"
                :value="customer.id"
              >
                {{ customer.c_name }}
              </a-select-option>
            </a-select>

            <a-button
              type="primary"
              class="fc-group-button blue"
              @click="showModal"
            >
              +
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
                <a-button key="back" @click="handleCancel" size="large">
                  Hủy
                </a-button>
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
        </a-col>
      </a-row>
      <a-row class="customer-select-row">
        <a-col :span="3">
          <div class="label">Ghi Chú:</div>
        </a-col>
        <a-col :span="21">
          <div class="text-area">
            <a-textarea
              @change="onChangeNote"
              placeholder="Ghi chú"
              :auto-size="{ minRows: 3, maxRows: 5 }"
            />
          </div>
        </a-col>
      </a-row>
    </div>
  </div>
</template>

<script>
import { mapState } from "vuex";
import moment from "moment";

import CustomerForm from "../../Customers/Core/CustomerForm.vue";

export default {
  name: "RepairCustomerSelect",
  components: {
    CustomerForm,
  },
  data() {
    return {
      isLoading: false,
      visible: false,
      isLoadingForm: false,
      form: this.$form.createForm(this),
    };
  },
  methods: {
    onSelectCustomer(val) {
      this.$emit("onSelectCustomer", val);
    },
    onChangeNote(e) {
      this.$emit("onChangeNote", e.target.value);
    },
    handleSearch(value) {
      this.isLoading = true;
      this.$store
        .dispatch("customers/getCustomers", {
          page: 1,
          perPage: 10,
          filter: value,
        })
        .then(() => (this.isLoading = false))
        .catch(() => (this.isLoading = false));
    },

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
          this.isLoadingForm = true;
          const bd = this.moment(values.c_birthday).format("DD/MM/YYYY");
          this.$store
            .dispatch("customers/createCustomer", {
              ...values,
              c_birthday: bd,
            })
            .then(() => {
              this.isLoadingForm = false;
              this.$message.success("Tạo mới khách hàng thành công");
              this.$emit("fetchData");
              this.handleCancel();
            })
            .catch((err) => {
              this.isLoadingForm = false;
              this.$message.error(err);
            });
        }
      });
    },
  },

  computed: {
    ...mapState({
      customers: (state) => state.customers.customers,
    }),
    dataCustomersSelect() {
      return JSON.parse(JSON.stringify(this.customers));
    },
  },
};
</script>

<style lang="less">
.repair-customer-select {
  margin-top: 1rem;
  &--select {
    margin-top: 1rem;
    .select-button {
      display: flex;
      align-items: center;
    }
    .fc-select-normal {
      width: 100%;
    }
    .label {
      white-space: nowrap;
    }
    .text-area {
      display: flex;
      align-items: center;

      margin: 1rem 0;
    }
  }
  .ant-row {
    display: flex;
    align-items: center;
  }
}
</style>
