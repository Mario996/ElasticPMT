<template>
  <v-row>
    <v-col cols="10"
           class="py-0 pl-3">
      <v-textarea
        v-model="comment.text"
        :disabled="!editing"
        solo
        no-resize
        height="8vh"
        name="input-7-4"
        label="Comment"
        :counter="500" />
    </v-col>
    <v-col cols="1"
           class="py-0 pl-1">
      <v-btn v-show="!editing"
             small
             rounded
             @click="editComment">
        <v-icon>mdi-pencil</v-icon>
      </v-btn>
      <v-btn v-show="editing"
             small
             rounded
             @click="saveEdit">
        <v-icon>mdi-content-save</v-icon>
      </v-btn>
    </v-col>
    <v-col cols="1"
           class="py-0">
      <v-btn small
             rounded
             @click="deleteComment">
        <v-icon>mdi-delete </v-icon>
      </v-btn>
    </v-col>
  </v-row>
</template>

<script>

export default {
    props: {
        comment: { type: Object, default: () => {} },
    },
    data: () => ({
        editing: false,
    }),
    methods: {
        editComment () {
            this.editing = true
        },
        saveEdit () {
            this.editing = false
            this.$emit('update-comment', this.comment)
        },
        deleteComment () {
            this.$emit('delete-comment', this.comment)
        }
    },
}
</script>

<style>
.theme--light.v-input--is-disabled input, .theme--light.v-input--is-disabled textarea{
    color: black;
}
</style>
