export const get = async function (url, axios, params) {
  let response = await axios.get(url, { params });
  return response.data;
};

export const post = async function (url, axios, params, config = {}) {
  let response = await axios.post(url, params, config);
  return response.data;
};

export const put = async function (url, axios, params, config = {}) {
  let response = await axios.put(url, params, config);
  return response.data;
};

export const del = async function (url, axios, params) {
  let response = await axios.delete(url, { params });
  return response.data;
};

export const editMutation = function (storeData, item) {
  let storeItem = storeData.find((i) => i.id == item.id);
  let index = storeData.indexOf(storeItem);
  storeData.splice(index, 1, item);
};
// NOTE: for store paginate information, will change params when have official api, snake case is needed
export const combinePageInformation = function (response) {
  if (!response.total_pages && !response.count)
    return {
      page: 1,
      per_page: 10,
      total_pages: 1,
      count: 0,
    };

  return {
    page: response.page,
    per_page: response.per_page,
    total_pages: response.total_pages,
    count: response.count,
  };
};

export const combineStatistic = function (statistic) {
  const {
    checking_count,
    quote_count,
    fixing_count,
    fixed_count,
    return_customer,
    expected_revenue,
  } = statistic;
  return {
    checking_count,
    quote_count,
    fixing_count,
    fixed_count,
    return_customer,
    expected_revenue,
  };
};
