<template>
  <div>
    <div class="fc-group-on-top">
      <div class="fc-group__left">
        <s-table-search
          v-on:onInputSearchSubmit="handleInputSearchSubmit"
          placeholder="Nhập mã sản phẩm hoặc tên sản phẩm"
        />
      </div>
      <div class="fc-group__right">
        <nuxt-link to="/products/new">
          <a-button type="primary" class="fc-group-button blue">
            + Tạo sản phẩm
          </a-button>
        </nuxt-link>
      </div>
    </div>
    <div class="fc-group-on-top">
      <div class="fc-group__left">
        <s-single-select
          :dataOptions="dataProductConditions"
          :defaultValue="product_conditions[0].id"
          v-on:onSelectOption="onSelectCondition"
          placeholder="Tình trạng sản phẩm"
        />
        <s-single-select
          :dataOptions="manufacturers"
          placeholder="Chọn nhà sản xuất"
          v-on:onSelectOption="onSelectManufacturer"
        />

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
      </div>
    </div>

    <s-table
      :columns="columns"
      :dataSource="dataSource"
      v-on:onTableChange="onTableChange"
      :loading="loading"
      :pagination="pagination"
      rowKey="pro_code"
    />
  </div>
</template>

<script>
import { mapState } from "vuex";

import STableSearch from "../../../Global/STableSearch.vue";
import STableSelectEntries from "../../../Global/STableSelectEntries.vue";
import STable from "../../../Global/STable";
import SSingleSelect from "../../../Global/SSingleSelect.vue";

export default {
  components: { STableSelectEntries, STableSearch, STable, SSingleSelect },
  props: {
    dataSource: {
      type: Array,
      default: [],
    },
    pagination: {
      type: Object,
    },
  },

  data() {
    return {
      columns: [
        {
          title: "Mã sản phẩm",
          dataIndex: "pro_code",
          sorter: true,
        },
        {
          title: "Tên sản phẩm",
          dataIndex: "pro_name",
          sorter: true,
        },
        {
          title: "SL",
          dataIndex: "pro_quanlity",
          sorter: true,
        },
        {
          title: "Giá vốn",
          dataIndex: "pro_sell_in",
          sorter: true,
        },
        {
          title: "Giá web",
          dataIndex: "pro_sell_out",
          sorter: true,
        },
        {
          title: "TTSP",
          dataIndex: "condition_name",
          sorter: true,
        },
        {
          title: "Bảo hành NCC",
          dataIndex: "pro_warranty",
          sorter: true,
        },
        {
          title: "Danh mục",
          dataIndex: "category_name",
          sorter: true,
        },
        {
          title: "Nhà sản xuất",
          dataIndex: "manufacturer_name",
          sorter: true,
        },
        {
          title: "Nhà cung cấp",
          dataIndex: "supplier_name",
          sorter: true,
        },
      ],
      loading: false,
      perPage: 10,
      page: 1,
      q: "",
      idProductCondition: null,
      idManufacturer: null,
      idCategory: null,
    };
  },
  methods: {
    fetch() {
      this.loading = true;

      const payload = {
        page: this.page,
        perPage: this.perPage,
        filter: this.q,
        idProductCondition: this.idProductCondition,
        idManuFacturer: this.idManufacturer,
        idCategory: this.idCategory,
      };

      this.$store
        .dispatch("products/getProducts", { ...payload })
        .then(() => (this.loading = false));
    },
    onTableChange(value) {
      if (this.page != value.pagination.current) {
        this.page = value.pagination.current;
      } else {
        this.page = 1;
      }
      this.fetch();
    },
    handleInputSearchSubmit(value) {
      this.page = 1;
      this.q = value;
      this.fetch();
    },
    onSelectCondition(idProductCondition) {
      this.page = 1;
      this.idProductCondition = idProductCondition;
      this.fetch();
    },
    onSelectManufacturer(idManufacturer) {
      this.page = 1;
      this.idManufacturer = idManufacturer;
      this.fetch();
    },
    onSelectCategory(idCategory) {
      this.page = 1;
      this.idCategory = idCategory[idCategory.length - 1];
      this.fetch();
    },
  },
  computed: {
    ...mapState({
      product_conditions: (state) => state.products.product_conditions,
      manufacturers: (state) => state.manufacturers.manufacturers,
      categories: (state) => state.categories.categories,
    }),
    dataProductConditions() {
      return this.product_conditions.map(({ id, cond_name }) => ({
        id,
        name: cond_name,
      }));
    },
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

<style lang="less" scoped>
</style>
