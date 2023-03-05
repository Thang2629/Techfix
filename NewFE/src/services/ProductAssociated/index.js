import * as api from "config/axios";

const PRODUCT_ASSOCIATED = "product-associated/combobox-data";

export const getProductAssicatedByType = (type) => {
  return api.sendGet(`${PRODUCT_ASSOCIATED}/${type}`);
};
