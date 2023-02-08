<template>
  <div>
    <a-form
      layout="vertical"
      :form="form"
      class="fc-reduce-space-form"
      @submit="handleSubmit"
    >
      <a-row>
        <a-form-item label="Tên sản phẩm">
          <a-input
            v-decorator="[
              'pro_name',
              {
                rules: [
                  { required: true, message: 'Vui lòng nhập tên sản phẩm' },
                ],
              },
            ]"
            placeholder="Nhập tên sản phẩm"
            size="large"
            allow-clear
          />
        </a-form-item>
      </a-row>
      <a-row type="flex" justify="space-between" :gutter="16">
        <a-col :xs="24" :lg="12">
          <a-form-item label="Mã sản phẩm">
            <a-input
              v-decorator="['pro_code']"
              placeholder="Nếu không nhập, hệ thống sẽ tự sinh"
              size="large"
              allow-clear
            />
          </a-form-item>

          <a-form-item label="Số lượng">
            <a-input
              v-decorator="['pro_quanlity', { initialValue: 0 }]"
              size="large"
              disabled
            />
          </a-form-item>

          <a-form-item label="Danh mục">
            <a-cascader
              style="width: 200px, height: 40px"
              @change="onSelectCategory"
              :options="dataProductCate"
              change-on-select
              size="large"
              class="fc-casader"
              placeholder="Chọn danh mục"
            >
            </a-cascader>
          </a-form-item>

          <a-form-item label="Đơn vị tính">
            <a-select
              size="large"
              v-decorator="['id_unit', { initialValue: product_units[0].id }]"
              :getPopupContainer="(trigger) => trigger.parentNode"
            >
              <a-select-option
                v-for="product_unit in product_units"
                :key="product_unit.id"
              >
                {{ product_unit.unit_name }}
              </a-select-option>
            </a-select>
          </a-form-item>

          <a-form-item label="Bảo hành NCC">
            <a-input
              v-decorator="['pro_warranty']"
              placeholder="Bảo hành NCC"
              size="large"
              allow-clear
            />
          </a-form-item>

          <a-form-item label="Nhà cung cấp">
            <a-select
              size="large"
              placeholder="Chọn nhà cung cấp"
              v-decorator="['id_sup']"
              :getPopupContainer="(trigger) => trigger.parentNode"
            >
              <a-select-option v-for="supplier in suppliers" :key="supplier.id">
                {{ supplier.name }}
              </a-select-option>
            </a-select>
          </a-form-item>

          <a-form-item label="Nhà sản xuất">
            <a-select
              size="large"
              placeholder="Chọn nhà sản xuất"
              v-decorator="['id_manu', { initialValue: manufacturers[0].id }]"
              :getPopupContainer="(trigger) => trigger.parentNode"
            >
              <a-select-option
                v-for="manufacturer in manufacturers"
                :key="manufacturer.id"
              >
                {{ manufacturer.name }}
              </a-select-option>
            </a-select>
          </a-form-item>
        </a-col>

        <a-col :xs="24" :lg="12">
          <a-form-item label="Định mức tối thiểu">
            <a-input-number
              v-decorator="['pro_min', { initialValue: 0 }]"
              :formatter="
                (value) => `$ ${value}`.replace(/\B(?=(\d{3})+(?!\d))/g, ',')
              "
              :parser="(value) => value.replace(/\$\s?|(,*)/g, '')"
              placeholder="0"
              size="large"
              allow-clear
            />
          </a-form-item>

          <a-form-item label="Giá nhập">
            <a-input
              v-decorator="['pro_original_cost', { initialValue: 0 }]"
              size="large"
              disabled
            />
          </a-form-item>

          <a-form-item label="Giá web">
            <a-input-number
              v-decorator="['pro_sell_in', { initialValue: 0 }]"
              :formatter="
                (value) => `$ ${value}`.replace(/\B(?=(\d{3})+(?!\d))/g, ',')
              "
              :parser="(value) => value.replace(/\$\s?|(,*)/g, '')"
              placeholder="0"
              size="large"
              allow-clear
            />
          </a-form-item>

          <a-form-item label="Giá vốn">
            <a-input-number
              v-decorator="['pro_sell_out', { initialValue: 0 }]"
              :formatter="
                (value) => `$ ${value}`.replace(/\B(?=(\d{3})+(?!\d))/g, ',')
              "
              :parser="(value) => value.replace(/\$\s?|(,*)/g, '')"
              placeholder="0"
              size="large"
              allow-clear
            />
          </a-form-item>

          <a-form-item label="Định mức tối đa">
            <a-input-number
              v-decorator="['pro_max', { initialValue: 0 }]"
              :formatter="
                (value) => `$ ${value}`.replace(/\B(?=(\d{3})+(?!\d))/g, ',')
              "
              :parser="(value) => value.replace(/\$\s?|(,*)/g, '')"
              placeholder="0"
              size="large"
              allow-clear
            />
          </a-form-item>

          <a-form-item label="Tình trạng sản phẩm">
            <a-select
              size="large"
              v-decorator="['id_con']"
              placeholder="Tình trạng sản phẩm"
              :getPopupContainer="(trigger) => trigger.parentNode"
            >
              <a-select-option
                v-for="cond in product_conditions"
                :key="cond.id"
              >
                {{ cond.cond_name }}
              </a-select-option>
            </a-select>
          </a-form-item>

          <a-form-item label="#">
            <div>
              <a-checkbox v-decorator="['is_inventory']">
                Theo dõi tồn kho?</a-checkbox
              >
              <a-checkbox v-decorator="['is_allownegative']">
                Cho phép bán âm?
              </a-checkbox>
            </div>
          </a-form-item>
        </a-col>
      </a-row>
      <div class="footer">
        <a-button size="large" type="default" @click="handleCancel"
          >Trở về</a-button
        >
        <a-button
          size="large"
          type="primary"
          html-type="submit"
          :loading="isLoading"
          >Lưu và tiếp tục</a-button
        >
      </div>
    </a-form>
  </div>
</template>

<script>
import { mapState } from "vuex";

export default {
  data() {
    return {
      form: this.$form.createForm(this),
      isLoading: false,
      cateId: "",
    };
  },
  methods: {
    handleSubmit(e) {
      e.preventDefault();
      this.form.validateFields((err, values) => {
        if (!err) {
          const payload = { ...values, id_cat: this.cateId };
          this.isLoading = true;
          this.$store
            .dispatch("products/createProduct", payload)
            .then(() => {
              this.$router.push({
                path: "/products/",
              });
              this.$message.success("Tạo sản phẩm thành công");
              this.isLoading = false;
            })
            .catch((err) => {
              this.$message.error(err.message);
              this.isLoading = false;
            });
        }
      });
    },
    handleCancel() {
      this.$router.push({
        path: "/products/",
      });
    },
    onSelectCategory(idCategory) {
      this.cateId = idCategory[idCategory.length - 1];
    },
  },
  computed: {
    ...mapState({
      product_units: (state) => state.products.product_units,
      categories: (state) => state.categories.categories,
      suppliers: (state) => state.suppliers.suppliers,
      manufacturers: (state) => state.manufacturers.manufacturers,
      product_conditions: (state) => state.products.product_conditions,
    }),
    dataProductCate() {
      const options = this.categories.map(
        ({ id, cat_name, categoryParents }) => ({
          value: id,
          label: cat_name,
          children: categoryParents.map(({ cat_name, id }) => ({
            value: id,
            label: cat_name,
          })),
        })
      );
      return options;
    },
  },
};
</script>

<style>
</style>