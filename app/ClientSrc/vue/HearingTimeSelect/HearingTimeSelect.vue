<template>
  <div class="row no-gutters">
    <div class="col">
      <div class="swiper-button-prev" slot="button-prev"></div>
      <swiper
        ref="mySwiper"
        :options="swiperOption"
        id="swipe-container"
        :auto-destroy="true"
        :delete-instance-on-destroy="true"
      >
        <swiper-slide v-for="entry in availabletimes" :key="entry.date">
          <div class="custom-slide-container">
            <div class="custom-slide-header text-center">
              {{ entry.weekday }}
              <br />
              {{ entry.formattedDate }}
            </div>
            <div class="custom-slide-times text-center">
              <div v-for="container in entry.times">
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
      </swiper>
      <div class="swiper-button-next" slot="button-next"></div>
      <input type="hidden" id="selectedDate" />
      <button type="button" class="btn btn-primary" id="slideBtn" hidden @click="toSlide()">
        Slide to selected date
      </button>
    </div>
  </div>
</template>

<script>
import Vue from "vue";
import VueAwesomeSwiper from "vue-awesome-swiper";

import { Swiper, SwiperSlide, directive } from "vue-awesome-swiper";
import "swiper/css/swiper.css";

import axios from "axios";

Vue.use(VueAwesomeSwiper);

export default {
  components: {
    Swiper,
    SwiperSlide,
  },
  directives: {
    swiper: directive,
  },
  props: {
    locationId: Number,
    availableDates: [],
    hearingType: Number,
  },
  data() {
    return {
      availabletimes: [],
      selectedContainerId: null,
      selectedBookingTime: null,
      swiperOption: {
        slidesPerView: 4,
        centeredSlides: false,
        spaceBetween: 16,
        grabCursor: true,
        pagination: {
          el: ".swiper-pagination",
          clickable: true,
        },
        navigation: {
          nextEl: ".swiper-button-next",
          prevEl: ".swiper-button-prev",
        },
        breakpoints: {
          1200: {
            slidesPerView: 4,
            spaceBetween: 8,
          },
          1000: {
            slidesPerView: 4,
            spaceBetween: 8,
          },
          765: {
            slidesPerView: 3,
            spaceBetween: 10,
          },
          610: {
            slidesPerView: 1,
            spaceBetween: 16,
          },
        },
      },
    };
  },
  computed: {
    swiper() {
      return this.$refs.mySwiper.$swiper;
    },
  },
  methods: {
    toSlide() {
      const i = $("#selectedDate").val();
      if (this.swiper) {
        this.swiper.slideTo(i, 0);
      }
    },
    keyboardSelection(containerId, bookingTime) {
      this.selectTime(containerId, bookingTime);
      $("#availableTimesForm").submit();
    },
    selectTime(containerId, bookingTime) {
      this.selectedContainerId = containerId;
      // store booking time as an ISO 8601 string
      this.selectedBookingTime = new Date(bookingTime).toISOString();

      //check if date is still available
      validateCaseDate(containerId, bookingTime);
    },
  },
  created() {
    let self = this;
    axios
      .get(
        `/scjob/booking/api/sc-available-dates-by-location/${this.locationId}/${this.hearingType}`
      )
      .then((response) => {
        self.availabletimes = response.data;
      });
  },
  mounted() {
    let self = this;
    if (this.swiper) {
      this.swiper.on("slideChange", function () {
        $("#datepicker").datepicker("setDate", self.availableDates[this.activeIndex]);
      });
    }
  },
};
</script>
