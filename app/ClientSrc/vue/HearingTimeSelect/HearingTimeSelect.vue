<template>
  <div class="row no-gutters">
    <div class="col">
      <swiper
        ref="mySwiper"
        :modules="modules"
        :centered-slides="false"
        :grab-cursor="true"
        :navigation="{
          nextEl: '.swiper-button-next',
          prevEl: '.swiper-button-prev',
        }"
        :breakpoints="{
          0: {
            slidesPerView: 1,
            spaceBetween: 16,
          },
          765: {
            slidesPerView: 3,
            spaceBetween: 10,
          },
          1000: {
            slidesPerView: 4,
            spaceBetween: 8,
          },
          1200: {
            slidesPerView: 4,
            spaceBetween: 16,
          },
        }"
        @swiper="onSwiper"
        @slideChange="onSlideChange"
      >
        <swiper-slide v-for="entry in availabletimes" :key="entry.date">
          <div class="custom-slide-container">
            <div class="custom-slide-header text-center">
              {{ entry.weekday }}
              <br />
              {{ entry.formattedDate }}
            </div>
            <div class="custom-slide-times text-center">
              <div v-for="container in entry.times" :key="container.containerId">
                <div class="btn-group" role="group">
                  <button
                    type="button"
                    class="btn btn-tertiary btn-block"
                    @mousedown="selectTime(container.containerId, container.startDateTime)"
                    @keypress.enter="
                      keyboardSelection(container.containerId, container.startDateTime)
                    "
                    :class="{ selected: container.containerId === selectedContainerId }"
                  >
                    {{ container.start }}–{{ container.end }}
                  </button>
                </div>
              </div>
            </div>
          </div>
        </swiper-slide>

        <!-- Navigation buttons -->
        <div class="swiper-button-prev"></div>
        <div class="swiper-button-next"></div>
      </swiper>

      <input type="hidden" id="selectedDate" />
      <button type="button" class="btn btn-primary" id="slideBtn" hidden @click="toSlide()">
        Slide to selected date
      </button>
    </div>
  </div>
</template>

<script>
import { Swiper, SwiperSlide } from "swiper/vue";
import { Navigation } from "swiper/modules";

// Import Swiper styles
import "swiper/css";
import "swiper/css/navigation";

import axios from "axios";

export default {
  name: "HearingTimeSelect",
  components: {
    Swiper,
    SwiperSlide,
  },
  props: {
    locationId: {
      type: Number,
      required: true,
    },
    availableDates: {
      type: Array,
      default: () => [],
    },
    hearingType: {
      type: Number,
      required: true,
    },
  },
  data() {
    return {
      availabletimes: [],
      selectedContainerId: null,
      selectedBookingTime: null,
      swiperInstance: null,
      modules: [Navigation],
    };
  },
  computed: {
    swiper() {
      return this.swiperInstance;
    },
  },
  methods: {
    onSwiper(swiper) {
      this.swiperInstance = swiper;
    },
    onSlideChange(swiper) {
      // Update datepicker when slide changes
      if (this.availableDates && this.availableDates[swiper.activeIndex]) {
        const $ = window.$;
        if ($ && $("#datepicker").length) {
          $("#datepicker").datepicker("setDate", this.availableDates[swiper.activeIndex]);
        }
      }
    },
    toSlide() {
      const $ = window.$;
      const i = $("#selectedDate").val();
      if (this.swiper && i !== '' && !isNaN(i)) {
        this.swiper.slideTo(Number(i), 0);
      }
    },
    keyboardSelection(containerId, bookingTime) {
      this.selectTime(containerId, bookingTime);
      const $ = window.$;
      if ($("#availableTimesForm").length) {
        $("#availableTimesForm").submit();
      }
    },
    selectTime(containerId, bookingTime) {
      this.selectedContainerId = containerId;
      // store booking time as an ISO 8601 string
      this.selectedBookingTime = new Date(bookingTime).toISOString();

      //check if date is still available
      if (window.validateCaseDate) {
        window.validateCaseDate(containerId, bookingTime);
      }
    },
  },
  async created() {
    try {
      const response = await axios.get(
        `/scjob/booking/api/sc-available-dates-by-location/${this.locationId}/${this.hearingType}`,
      );
      this.availabletimes = response.data;
    } catch (error) {
      console.error("Error fetching available times:", error);
    }
  },
};
</script>
